using C64.Data;
using C64.Data.Archive;
using C64.Data.Storage;
using D64Reader;
using D64Reader.Renderers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
        private readonly ILogger<DownloadController> logger;

        private readonly string productionContainer = "productionfiles";
        private readonly string pictureContainer = "productionpictures";
        private static int cacheTtl = 24 * 60 * 60 * 365; // 1 year

        public DownloadController(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, ILogger<DownloadController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.fileStorageService = fileStorageService;
            this.logger = logger;
        }

        [Route("/demos/download.php")]
        public async Task<IActionResult> LegacyDownload(int id)
        {
            var prodFile = await unitOfWork.Productions.GetProductionFileByProductionId(id);
            if (prodFile == null)
                return NotFound();

            return await Download(productionContainer, prodFile.Filename);
        }

        [Route("/data/{container}/{fileName}")]
        public async Task<IActionResult> Download(string container, string fileName)
        {
            try
            {
                var file = await fileStorageService.GetFileContents(container, fileName);
                if (container == productionContainer)
                {
                    await unitOfWork.Productions.AddDownload(fileName, RemoteIp, Referer, UserId);
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
                var d64Reader = new D64ReaderCore(archiveService.GetFile(d64FileName));
                return new FileContentResult(d64Reader.Render(new D64PngRenderer()), "image/png");
            }
            catch
            {
                fallbackArchiveService.Load(fileData);
                var d64FileName = fallbackArchiveService.ArchiveInfo.CompressedFileInfos.Where(p => p.IsD64).ElementAt(counter).FileName;
                var d64Reader = new D64ReaderCore(fallbackArchiveService.GetFile(d64FileName));
                return new FileContentResult(d64Reader.Render(new D64PngRenderer()), "image/png");
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
            if (x < 1 || x > 1920 || y < 1 || y > 1080)
                throw new ArgumentOutOfRangeException("invalid size");

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
                        var dstRectangle = new Rectangle(offsetX, offsetY, (int)(source.Width * ratio), (int)(source.Height * ratio));

                        graphics.DrawImage(source, dstRectangle, srcRectangle, GraphicsUnit.Pixel);
                    }

                    // We always return JPEG, regardless of the original format. Browsers can handle that.
                    using (var stream = new MemoryStream())
                    {
                        // Set JPG-quality the Microsoft-Way....
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

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

        private ImageCodecInfo GetEncoder(ImageFormat format)
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

        private float Ratio(Bitmap source, Bitmap destination)
        {
            return Math.Min((float)destination.Width / source.Width, (float)destination.Height / source.Height);
        }

        private float Ratio(int sourceWidth, int sourceHeight, int maxWidth, int maxHeight)
        {
            return Math.Min((float)maxWidth / sourceWidth, (float)maxHeight / sourceHeight);
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(fileName, out var mimeType);

            return mimeType ?? "application/octet-stream";
        }
    }
}