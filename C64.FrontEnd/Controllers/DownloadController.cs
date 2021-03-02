using C64.Data;
using C64.FrontEnd.Extensions;
using C64.Services.Archive;
using C64.Services.Storage;
using D64Reader.Renderers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    /// <summary>
    /// Kinda emulating the filesystem, also updates downloads if needed
    /// </summary>
    public class DownloadController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileStorageService fileStorageService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string productionContainer = "productionfiles";
        private readonly string pictureContainer = "productionpictures";
        private readonly static int cacheTtl = 24 * 60 * 60 * 365; // 1 year

        public DownloadController(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.fileStorageService = fileStorageService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Route("/demos/download.php")]
        public async Task<IActionResult> LegacyDownload(int id)
        {
            var prodFile = await unitOfWork.Productions.GetProductionFileByProductionId(id);
            if (prodFile == null)
                return NotFound();
            try
            {
                var container = productionContainer;
                var fileName = prodFile.Filename;
                var file = await fileStorageService.GetFileContents(container, fileName);
                if (container == productionContainer)
                {
                    await unitOfWork.Productions.AddDownload(fileName, httpContextAccessor.HttpContext.RemoteIp(), httpContextAccessor.HttpContext.Referer(), httpContextAccessor.HttpContext.GetUserId());
                    await unitOfWork.Commit();
                }

                return File(file, GetContentType(fileName), fileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("/data/{container}/{fileName}")]
        public async Task<IActionResult> Download(string container, string fileName)
        {
            try
            {
                var file = await fileStorageService.GetFileContents(container, fileName);
                if (container == productionContainer)
                {
                    await unitOfWork.Productions.AddDownload(fileName, httpContextAccessor.HttpContext.RemoteIp(), httpContextAccessor.HttpContext.Referer(), httpContextAccessor.HttpContext.GetUserId());
                    await unitOfWork.Commit();
                }

                return new FileContentResult(file, GetContentType(fileName));
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("/data/productions/diskimage/d64-{fileId}-{counter}.png")]
        public async Task<IActionResult> GetD64Image(int fileId, int counter, [FromServices] IArchiveService archiveService, [FromServices] IFallbackArchiveService fallbackArchiveService)
        {
            var productionFile = await unitOfWork.Productions.GetFile(fileId);

            var fileData = await fileStorageService.GetFileContents(productionContainer, productionFile.Filename);

            try
            {
                archiveService.Load(fileData);
                var d64FileName = archiveService.ArchiveInfo.CompressedFileInfos.Where(p => p.IsD64).ElementAt(counter).FileName;
                var d64Reader = new D64Reader.D64ReaderCore(archiveService.GetFile(d64FileName));
                return new FileContentResult(d64Reader.Render(new D64PngRenderer()), "image/png");
            }
            catch
            {
                fallbackArchiveService.Load(fileData);
                var d64FileName = fallbackArchiveService.ArchiveInfo.CompressedFileInfos.Where(p => p.IsD64).ElementAt(counter).FileName;
                var d64Reader = new D64Reader.D64ReaderCore(fallbackArchiveService.GetFile(d64FileName));
                return new FileContentResult(d64Reader.Render(new D64PngRenderer()), "image/png");
            }
        }

        [Route("/data/productions/archive/{fileId}-{counter}.bin")]
        public async Task<IActionResult> GetArchiveFile(int fileId, int counter, [FromServices] IArchiveService archiveService, [FromServices] IFallbackArchiveService fallbackArchiveService)
        {
            var productionFile = await unitOfWork.Productions.GetFile(fileId);
            var fileData = await fileStorageService.GetFileContents(productionContainer, productionFile.Filename);

            try
            {
                archiveService.Load(fileData);
                var fileName = archiveService.ArchiveInfo.CompressedFileInfos.ElementAt(counter).FileName;
                var fileContents = archiveService.GetFile(fileName);
                return new FileContentResult(fileContents, "application/octet-stream");
            }
            catch
            {
                fallbackArchiveService.Load(fileData);
                var fileName = fallbackArchiveService.ArchiveInfo.CompressedFileInfos.ElementAt(counter).FileName;
                var fileContents = fallbackArchiveService.GetFile(fileName);
                return new FileContentResult(fileContents, "application/octet-stream");
            }
        }

        [Route("data/thumbnails/{x}x{y}")]
        public IActionResult GetThumb()
        {
            return Redirect("~/images/nopicture.jpg");
        }

        [Route("data/thumbnails/{x}x{y}/{filename}")]
        public async Task<IActionResult> GetThumb(int x, int y, string filename)
        {
            if (x < 1 || x > 1920)
                throw new ArgumentOutOfRangeException(nameof(x), "invalid size");

            if (y < 1 || y > 1080)
                throw new ArgumentOutOfRangeException(nameof(y), "invalid size");

            byte[] fileData;
            FileInformation fileInformation;

            try
            {
                fileData = await fileStorageService.GetFileContents(pictureContainer, filename);
                fileInformation = await fileStorageService.GetFileInformations(pictureContainer, filename);
            }
            catch (FileNotFoundException)
            {
                Log.Warning("Cannot find file for {pictureContainer} {filename}");
                return Redirect("/images/nopicture.jpg");
            }

            using (var image = new Bitmap(x, y))
            {
                using (var source = new Bitmap(new MemoryStream(fileData)))
                {
                    using (var graphics = Graphics.FromImage(image))
                    {
                        var ratio = Ratio(source, image);

                        var offsetX = (int)((image.Width - (source.Width * ratio)) / 2);
                        var offsetY = (int)((image.Height - (source.Height * ratio)) / 2);

                        var srcRectangle = new Rectangle(0, 0, source.Width, source.Height);
                        var dstRectangle = new Rectangle(offsetX, offsetY, (int)(1 + (source.Width * ratio)), (int)(1 + (source.Height * ratio)));

                        graphics.DrawImage(source, dstRectangle, srcRectangle, GraphicsUnit.Pixel);
                    }

                    // We always return JPEG, regardless of the original format. Browsers can handle that.
                    using (var stream = new MemoryStream())
                    {
                        // Set JPG-quality the Microsoft-Way....
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);

                        image.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

                        Response.Headers.Add("Cache-Control", $"public, max-age={cacheTtl}");

                        return new FileContentResult(stream.ToArray(), "image/jpeg")
                        {
                            EnableRangeProcessing = true,
                            LastModified = fileInformation.Created
                        };
                    }
                }
            }
        }

        [Route("/data/vice.ini")]
        public IActionResult DownloadViceIni(int sidModel = 0, int driveSoundEmulation = 0, int driveSoundEmulationVolume = 0, int crtEmulation = 0)
        {
            try
            {
                var content = System.IO.File.ReadAllLines("Data/vice.ini").ToList();

                content.Add($"SidModel={sidModel}");
                content.Add($"DriveSoundEmulation={driveSoundEmulation}");
                content.Add($"DriveSoundEmulationVolume={driveSoundEmulationVolume}");
                content.Add($"VICIIFilter={crtEmulation}");

                return new FileContentResult(Encoding.ASCII.GetBytes(string.Join("\n", content)), "text/plain");
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private static float Ratio(Bitmap source, Bitmap destination)
        {
            return Math.Min((float)destination.Width / source.Width, (float)destination.Height / source.Height);
        }

        private static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(fileName, out var mimeType);

            return mimeType ?? "application/octet-stream";
        }
    }
}