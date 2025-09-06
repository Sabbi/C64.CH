using C64.Data;
using C64.Data.Entities;
using C64.FrontEnd.Extensions;
using C64.FrontEnd.Helpers;
using C64.Services.Archive;
using C64.Services.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Build.ObjectModelRemoting;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    /// <summary>
    /// Kinda emulating the filesystem, also updates downloads if needed
    /// </summary>
    public class ApiController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileStorageService fileStorageService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiController(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.fileStorageService = fileStorageService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Route("/api/randomproduction")]
        public async Task<IActionResult> RandomProduction()
        {
            var production = await unitOfWork.Productions.GetRandomProduction();

            var detail = await unitOfWork.Productions.GetDetails(production.Id);

            var dto = new DtoRandomProduction()
            {
                Id = detail.Id,
                Name = detail.Name,
                Url = "https://c64.ch/productions/" + detail.Id + "/" + detail.Name.UrlEncode(),
                ImageUrls = detail.ProductionPictures.Select(p => "https://c64.ch/data/productionpictures/" + p.Filename).ToArray(),
                FileUrls = detail.ProductionFiles.Select(p => "https://c64.ch/data/productionfiles/" + p.Filename).ToArray(),
                YouTubeIds = detail.ProductionVideos.Select(p => p.VideoId).ToArray(),
            };


            return Ok(dto);

         
        }      
        private class DtoRandomProduction
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Url { get; set; }

            public string[] ImageUrls { get; set; }
            public string[] FileUrls { get; set; }

            public string[] YouTubeIds { get; set; }
        }
    }
}