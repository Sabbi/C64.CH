using C64.Data;
using C64.FrontEnd.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C64.FrontEnd.Controllers
{
    public class SitemapController : Controller
    {
        private readonly string[] sitemaps = { "sitemap_main.xml", "sitemap_news.xml", "sitemap_productions.xml", "sitemap_toplists.xml", "sitemap_groups.xml", "sitemap_sceners.xml", "sitemap_parties.xml", "sitemap_downloads.xml" };
        private readonly string staticPagesFile = "Data/staticpages.txt";
        private readonly string baseUrl = "https://c64.ch";

        private readonly int gridPagesize = 24;
        private readonly IUnitOfWork unitOfWork;
        private readonly XmlWriterSettings writerSettings;

        public SitemapController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            writerSettings = new XmlWriterSettings();
            writerSettings.Encoding = Encoding.UTF8;
            writerSettings.Indent = true;
        }

        [Route("/sitemap.xml")]
        public IActionResult Index()
        {
            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("sitemapindex", "http://www.google.com/schemas/sitemap/0.9");
                    writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");

                    foreach (var map in sitemaps)
                        AddSiteMap(writer, $"https://c64.ch/{map}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_main.xml")]
        public IActionResult StaticPages()
        {
            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    AddStaticPages(writer);

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_news.xml")]
        public async Task<IActionResult> News()
        {
            // News have a pagesize of 10
            var firstPageSitenews = await unitOfWork.SiteInfos.FindPaginated(p => p.Category == 0, "PublishedAt", true, 1, 10);
            var firstPageSceneNews = await unitOfWork.SiteInfos.FindPaginated(p => p.Category == 1, "PublishedAt", true, 1, 10);

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    for (var i = 1; i <= firstPageSceneNews.NumberOfPages; i++)
                        AddUrl(writer, i == 1 ? "/scenenews" : $"/scenenews/{i}");

                    for (var i = 1; i <= firstPageSitenews.NumberOfPages; i++)
                        AddUrl(writer, i == 1 ? "/sitenews" : $"/sitenews/{i}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_parties.xml")]
        public IActionResult Parties()
        {
            var parties = unitOfWork.Parties.GetAll();

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    foreach (var party in parties)
                        AddUrl(writer, $"/parties/{party.Id}/{party.Name.UrlEncode()}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_sceners.xml")]
        public IActionResult Sceners()
        {
            var sceners = unitOfWork.Sceners.GetAll();

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    foreach (var scener in sceners)
                        AddUrl(writer, $"/sceners/{scener.ScenerId}/{scener.Handle.UrlEncode()}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_groups.xml")]
        public IActionResult Groups()
        {
            var groups = unitOfWork.Groups.GetAll();

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    foreach (var group in groups)
                    {
                        for (var i = 1; i <= (group.NumberOfReleases / gridPagesize) + 1; i++)
                        {
                            if (i == 1)
                                AddUrl(writer, $"/groups/{group.GroupId}/{group.Name.UrlEncode()}");
                            else
                                AddUrl(writer, $"/groups/{group.GroupId}/{group.Name.UrlEncode()}?currentPage={i}&sortCol=Name&sortDir=True");
                        }
                    }

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_productions.xml")]
        public IActionResult Productions()
        {
            var productions = unitOfWork.Productions.GetAll();

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    foreach (var production in productions)
                        AddUrl(writer, $"/productions/{production.ProductionId}/{production.Name.UrlEncode()}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_downloads.xml")]
        public IActionResult Downloads()
        {
            var productions = unitOfWork.Productions.GetAll();

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    foreach (var production in productions)
                        AddUrl(writer, $"/demos/download.php?id={production.ProductionId}");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_toplists.xml")]
        public async Task<IActionResult> TopLists()
        {
            var productionIds = (await unitOfWork.Productions.Find(p => true)).Select(p => p.ProductionId).ToList();

            var pages = (productionIds.Count() / gridPagesize) + 1;

            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    AddGridPages(writer, "latestadded", pages, "Added", "False");
                    AddGridPages(writer, "latestreleased", pages, "ReleaseDate", "False");
                    AddGridPages(writer, "topdownloaded", pages, "Downloads", "False");
                    AddGridPages(writer, "toprated", pages, "AverageRating", "False");

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        [Route("/sitemap_otherlists.xml")]
        public async Task<IActionResult> Otherlists()
        {
            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, writerSettings))
                {
                    writer.WriteStartElement("urlset", "http://www.google.com/schemas/sitemap/0.9");

                    await AddReleasesByYearList(writer);
                    await AddReleasesByLetterList(writer);
                    await AddGroupReleasesByLetterList(writer);

                    writer.WriteEndElement();
                }
                return Content(Encoding.Default.GetString(output.ToArray()), "application/xml");
            }
        }

        private async Task AddReleasesByYearList(XmlWriter writer)
        {
            var releasesPerYear = await unitOfWork.Productions.GetNumberOfReleasesPerYear();
            foreach (var year in releasesPerYear.OrderBy(p => p.Key))
                AddGridPages(writer, $"year/{year.Key}", (year.Value / 24) + 1, "Added", "False");
        }

        private async Task AddReleasesByLetterList(XmlWriter writer)
        {
            var releasesPerLetter = await unitOfWork.Productions.GetNumberOfReleasesPerLetter();
            foreach (var letter in releasesPerLetter.OrderBy(p => p.Key))
                AddGridPages(writer, $"demoname/{letter.Key}", (letter.Value / 24) + 1, "Added", "False");
        }

        private async Task AddGroupReleasesByLetterList(XmlWriter writer)
        {
            var releasesPerLetter = await unitOfWork.Groups.GetNumberOfReleasesPerLetter();
            foreach (var letter in releasesPerLetter.OrderBy(p => p.Key))
                AddGridPages(writer, $"groupname/{letter.Key}", (letter.Value / 24) + 1, "Added", "False");
        }

        private void AddGridPages(XmlWriter writer, string gridtype, int pages, string sortCol, string sortDir)
        {
            for (var i = 1; i <= pages; i++)
            {
                if (i == 1)
                    AddUrl(writer, $"/productions/demos/{gridtype}");
                else
                    AddUrl(writer, $"/productions/demos/{gridtype}/?currentPage={i}&sortCol={sortCol}&sortDir={sortDir}");
            }
        }

        private void AddStaticPages(XmlWriter writer)
        {
            var staticPages = System.IO.File.ReadAllLines(staticPagesFile);

            foreach (var staticPage in staticPages)
            {
                AddUrl(writer, staticPage);
            }
        }

        private void AddSiteMap(XmlWriter writer, string address)
        {
            writer.WriteStartElement("sitemap");
            writer.WriteElementString("loc", address);
            writer.WriteEndElement();
        }

        private void AddUrl(XmlWriter writer, string url)
        {
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", $"{baseUrl}{url}");
            writer.WriteEndElement();
        }
    }
}