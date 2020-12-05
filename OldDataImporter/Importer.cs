using C64.Data;
using C64.Data.Entities;
using C64.Data.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OldDataImporter
{
    public class Importer
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly OldApplicationDbContext _oldDbContext;

        private readonly IUnitOfWork _unitOfWork;

        private Dictionary<int, string> _countryLookup = new Dictionary<int, string>();

        //private IHostingEnvironment _hostingEnvironment;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IFileStorageService _fileStorageService;

        private readonly IServiceProvider _serviceProvider;

        private readonly IConfiguration _configuration;

        public Importer()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(_configuration);

            var defaultConnectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(defaultConnectionString, ServerVersion.AutoDetect(defaultConnectionString), p => p.MigrationsAssembly("C64.Data")), contextLifetime: ServiceLifetime.Transient);

            var oldConnectionString = _configuration.GetConnectionString("OldConnection");
            services.AddDbContext<OldApplicationDbContext>(options => options.UseMySql(oldConnectionString, ServerVersion.AutoDetect(oldConnectionString), p => p.MigrationsAssembly("C64.Data")), contextLifetime: ServiceLifetime.Transient);

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.,_@+ /\\[](){}#*!=$£|àäüöéèôîêâáóôòñ^'Ëåç&:;ÊèíîÌàíÑàíêëàðòàÊÃðèãîðèéÈÉ";
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddLogging(options => options.AddDebug());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<C64.Services.Archive.IArchiveService, C64.Services.Archive.SharpZipArchiveService>();
            services.AddScoped<IFileStorageService, DbFileStorageService>();

            _serviceProvider = services.BuildServiceProvider();

            _dbContext = _serviceProvider.GetService<ApplicationDbContext>();
            _oldDbContext = _serviceProvider.GetService<OldApplicationDbContext>();

            _userManager = _serviceProvider.GetService<UserManager<User>>();
            _roleManager = _serviceProvider.GetService<RoleManager<IdentityRole>>();

            _unitOfWork = _serviceProvider.GetService<IUnitOfWork>();

            _fileStorageService = _serviceProvider.GetService<IFileStorageService>();

            _countryLookup.Add(2, "AL");
            _countryLookup.Add(12, "AU");
            _countryLookup.Add(13, "AT");
            _countryLookup.Add(20, "BE");
            _countryLookup.Add(36, "CA");
            _countryLookup.Add(59, "DK");
            _countryLookup.Add(73, "LI");
            _countryLookup.Add(74, "FR");
            _countryLookup.Add(80, "DE");
            _countryLookup.Add(95, "HU");
            _countryLookup.Add(114, "LV");
            _countryLookup.Add(119, "LI");
            _countryLookup.Add(121, "LU");
            _countryLookup.Add(144, "NL");
            _countryLookup.Add(154, "NO");
            _countryLookup.Add(165, "PL");
            _countryLookup.Add(188, "SK");
            _countryLookup.Add(199, "SE");
            _countryLookup.Add(200, "CH");
            _countryLookup.Add(205, "MK");
            _countryLookup.Add(212, "TK");
            _countryLookup.Add(219, "GB");
        }

        public Task<bool> CreateDatabase()
        {
            _dbContext.Database.Migrate();
            return Task.FromResult(true);
        }

        public async Task<bool> ImportUserAsync()
        {
            //return Content("already done");
            var oldUsers = _oldDbContext.OldUsers.OrderBy(p => p.ID);

            var adddata = _oldDbContext.OldUserDatas.ToList();

            foreach (var oldUser in oldUsers)
            {
                if (oldUser.Email != null && oldUser.Email.Contains("@"))
                {
                    var add = adddata.FirstOrDefault(p => p.UserId == oldUser.ID);

                    if (oldUser.LastLogin == null || oldUser.DateReg < DateTime.MinValue)
                        oldUser.LastLogin = DateTime.MinValue;

                    if (oldUser.DateReg == null || oldUser.DateReg < DateTime.MinValue)
                        oldUser.DateReg = DateTime.MinValue;

                    if (add != null)
                    {
                        if (add.Birthday == null)
                            add.Birthday = DateTime.MinValue;
                    }

                    var newUser = new User
                    {
                        UserName = oldUser.Username,
                        Email = oldUser.Email,
                        OldId = oldUser.ID,
                        Newsletter = oldUser.Newsletter != null,
                        Registered = oldUser.DateReg.GetValueOrDefault(),
                        LastLogin = oldUser.LastLogin.GetValueOrDefault()
                    };

                    if (add != null)
                    {
                        newUser.Realname = add.Realname;
                        newUser.PublicEmail = add.Email;
                        newUser.Homepage = add.Homepage;
                        newUser.Icq = add.ICQ;
                        newUser.Birthday = add.Birthday.GetValueOrDefault();
                        newUser.Location = add.Location;
                        newUser.Occupation = add.Occupation;
                        newUser.Groups = add.Groups;
                        newUser.FavDemos = add.FavDemo;
                        newUser.FavGroups = add.FavGroup;
                        newUser.Watching = add.Watching;
                        newUser.Blabla = add.Blabla;
                    }

                    var result = await _userManager.CreateAsync(newUser, oldUser.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var mist in result.Errors)
                        {
                            Debug.WriteLine(mist.Description);
                        }
                    }
                }

                Console.Write(".");
                Debug.Write(oldUser.ID);
                Debug.Write(" / ");
            }

            return true;
        }

        public async Task<bool> AddRoles()
        {
            var r1 = await AddRole("Admin");
            var r2 = await AddRole("Moderator");

            return r1 && r2;
        }

        private async Task<bool> AddRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return false;

            var exists = await _roleManager.RoleExistsAsync(roleName);

            if (!exists)
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
                return true;
            }

            return false;
        }

        public async Task<bool> AddToAdmin()
        {
            //return Content("Done");

            var user = await _userManager.FindByEmailAsync("d@c64.ch");

            if (user == null)
                return false;

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            var result2 = await _userManager.AddToRoleAsync(user, "Moderator");

            if (result == IdentityResult.Success)
                return true;

            Console.Write("Something failed:");
            Console.WriteLine(result.ToString());
            return false;
        }

        public async Task<bool> ImportGuestbook()
        {
            var oldGuestbook = _oldDbContext.OldGuestbooks.OrderBy(p => p.Id);

            foreach (var entry in oldGuestbook)
            {
                var newEntry = new GuestbookEntry
                {
                    GuestbookEntryId = entry.Id,
                    Comment = entry.Comment,
                    Email = entry.Email,
                    Homepage = entry.Homepage,
                    Name = entry.Name,
                    WrittenAt = entry.Datum.GetValueOrDefault()
                };
                Console.Write(".");
                _unitOfWork.GuestbookEntries.Add(newEntry);
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> ImportSiteInfos()
        {
            // return Content("already done");
            var oldNewsarr = _oldDbContext.OldNews.OrderBy(p => p.Id);

            foreach (var oneNews in oldNewsarr)
            {
                string userId = null;
                if (!string.IsNullOrEmpty(oneNews.Writer))
                {
                    var user = _dbContext.Users.FirstOrDefault(p => p.UserName == oneNews.Writer);
                    if (user != null)
                        userId = user.Id;
                }

                var siteInfo = new SiteInfo
                {
                    SiteInfoId = oneNews.Id,
                    PublishedAt = oneNews.TimeStamp,
                    Text = oneNews.Text,
                    Title = oneNews.Title,
                    UserId = userId,
                    Writer = oneNews.Writer
                };
                Console.Write(".");
                _unitOfWork.SiteInfos.Add(siteInfo);
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> ImportLinks()
        {
            var oldLinks = _oldDbContext.OldLinks.OrderBy(p => p.Id);

            foreach (var oldLink in oldLinks)
            {
                var newLink = new Link
                {
                    LinkId = oldLink.Id,
                    Added = oldLink.Zeit.GetValueOrDefault(),
                    Hits = oldLink.Hits.GetValueOrDefault(),
                    LinkCategoryId = oldLink.GruppeId,
                    Name = oldLink.Name,
                    Url = oldLink.Url
                };

                Console.Write(".");
                _unitOfWork.Links.Add(newLink);
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> StoreFiles()
        {
            var path = "demos";
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var filename = Path.GetFileName(file);
                await _fileStorageService.SaveFile(bytes, "productionfiles", filename);
                Console.Write(".");
            }
            return true;
        }

        public async Task<bool> StorePictures()
        {
            var path = "picorig";
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var filename = Path.GetFileName(file);
                await _fileStorageService.SaveFile(bytes, "productionpictures", filename);
                Console.Write(".");
            }
            return true;
        }

        public async Task<bool> ImportGroups()
        {
            //return Content("already done");
            var groups = _oldDbContext.OldGroups;

            foreach (var group in groups)
            {
                var gr = new Group
                {
                    GroupId = group.GroupId,
                    Added = group.GroupAddedAt ?? DateTime.MinValue,
                    Modified = group.GroupLastModifiedAt ?? DateTime.MinValue,
                    Name = group.GroupName,
                };

                Console.Write(".");
                _unitOfWork.Groups.Add(gr);
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> ImportParties()
        {
            //return Content("already done");
            // Check country lookup
            var oldParties = _oldDbContext.OldParties;

            foreach (var oldParty in oldParties)
            {
                var countryId = oldParty.CountryId;

                if (countryId != null)
                {
                    // Throws if not found
                    _countryLookup.First(p => p.Key == countryId);
                }
            }

            foreach (var oldParty in oldParties)
            {
                var party = new Party
                {
                    PartyId = oldParty.Id,
                    Name = oldParty.Name,
                    From = oldParty.From,
                    To = oldParty.To,
                    Description = oldParty.Information,
                    Url = oldParty.Url,
                    Email = oldParty.Email,
                    Organizers = oldParty.Organizer,
                    Location = oldParty.Location,
                    CountryId = oldParty.CountryId == null ? null : _countryLookup.First(p => p.Key == oldParty.CountryId).Value
                };

                // Due to a display-but, start/end of many parties are wrong...
                if (oldParty.To < oldParty.From)
                {
                    party.To = oldParty.From;
                    party.From = oldParty.To;
                }

                Console.Write(".");
                _dbContext.Parties.Add(party);

                await _dbContext.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> ImportDemos()
        {
            var demos = _oldDbContext.OldDemos;

            foreach (var demo in demos)
            {
                Debug.WriteLine("Now: " + demo.Name);
                var foo = demo;
                var nd = new Production
                {
                    ProductionId = demo.Id,
                    Name = demo.Name,
                    Remarks = string.IsNullOrEmpty(demo.Remarks) || demo.Remarks == "n" ? null : demo.Remarks,
                    Uploader = string.IsNullOrEmpty(demo.Uploader) ? null : demo.Uploader,
                    Submitter = string.IsNullOrEmpty(demo.Submitter) ? null : demo.Submitter,
                    VideoType = demo.Video.HasValue ? (VideoType)demo.Video.Value : 0,
                    Added = demo.UploadDate,
                    TopCategory = TopCategory.Demos,
                    SubCategory = SubCategory.Demo
                };

                // Year
                if (demo.Year == 0)
                {
                    nd.ReleaseDate = DateTime.MinValue;
                    nd.ReleaseDateType = DateType.None;
                }
                else if (demo.Month == 0)
                {
                    nd.ReleaseDate = new DateTime(demo.Year, 1, 1);
                    nd.ReleaseDateType = DateType.Year;
                }
                else if (demo.Day == 0)
                {
                    nd.ReleaseDate = new DateTime(demo.Year, demo.Month, 1);
                    nd.ReleaseDateType = DateType.YearMonth;
                }
                else
                {
                    try
                    {
                        nd.ReleaseDate = new DateTime(demo.Year, demo.Month, demo.Day);
                        nd.ReleaseDateType = DateType.YearMonthDay;
                    }
                    catch
                    {
                        nd.ReleaseDate = DateTime.MinValue;
                        nd.ReleaseDateType = DateType.None;
                    }
                }

                if (demo.Location != null && demo.Location.Length > 1000)
                {
                    throw new ArgumentException("Demo " + demo.Id + " Filename too long");
                }

                try
                {
                    var fileExists = await _fileStorageService.GetFileInformations("productionfiles", demo.Location);

                    if (fileExists != null)
                    {
                        // File
                        nd.ProductionFiles.Add(new ProductionFile
                        {
                            Filename = demo.Location,
                            Downloads = demo.Downloads,
                            Size = (int)fileExists.FileSize
                        });
                    }
                    else
                    {
                        Console.WriteLine($"File {demo.Location} for {demo.Name} not found");
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message + " / " + ex.FileName);
                }

                // Pictures
                if (demo.Pictures != null)
                {
                    foreach (var picture in demo.Pictures)
                    {
                        var replpicture = picture?.Replace(".gif", ".png");

                        try
                        {
                            var pictureExists = await _fileStorageService.GetFileInformations("productionpictures", replpicture);

                            if (pictureExists != null)
                            {
                                nd.ProductionPictures.Add(new ProductionPicture
                                {
                                    Filename = replpicture,
                                    Size = (int)pictureExists.FileSize
                                });
                            }
                            else
                            {
                                Console.WriteLine($"File {picture} for {demo.Name} not found");
                            }
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message + " / " + ex.FileName);
                        }
                    }
                }

                // Hidden Parts
                if (!string.IsNullOrEmpty(demo.HiddenParts))
                {
                    nd.HiddenParts.Add(new HiddenPart
                    {
                        Description = demo.HiddenParts
                    });
                }
                Console.Write(".");
                _unitOfWork.Productions.Add(nd);
            }

            await _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> DemosToGroups()
        {
            // return Content("already done");
            var demos = _unitOfWork.Productions.GetAll();

            foreach (var demo in demos)
            {
                var oldDemo = _oldDbContext.OldDemos.First(p => p.Id == demo.ProductionId);

                if (oldDemo == null)
                    throw new NullReferenceException("Old Demo does not exist!");

                var groups = oldDemo.GroupIds;

                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        var exists = await _unitOfWork.Groups.Find(p => p.GroupId == group);

                        if (exists != null)
                        {
                            demo.ProductionsGroups.Add(new ProductionsGroups
                            {
                                GroupId = group
                            });
                        }
                    }
                }
                Console.Write(".");
            }

            await _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> ImportDemoParty()
        {
            var links = _oldDbContext.OldPartyLinks;
            var parties = _unitOfWork.Parties.GetAll();

            // Add all partycategories to db
            _dbContext.PartyCategories.AddRange(PartyCategories);

            foreach (var link in links)
            {
                if (parties.FirstOrDefault(p => p.PartyId == link.PartyId) != null)
                {
                    var demoParty = new ProductionsParties
                    {
                        ProductionId = link.DemoId,
                        PartyId = link.PartyId,
                        Rank = link.Ranking.GetValueOrDefault(),
                        PartyCategoryId = PartyCategoryLookup(link.Category)
                    };
                    _dbContext.ProductionsParties.Add(demoParty);
                }
                Console.Write(".");
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private List<PartyCategory> PartyCategories = new List<PartyCategory>
        {
            new PartyCategory { PartyCategoryId = 1, Name= "256b Intro", Selectable = true},
            new PartyCategory { PartyCategoryId = 2, Name= "4kb Intro", Selectable = true},
            new PartyCategory { PartyCategoryId = 3, Name= "Alternative Demo", Selectable = true},
            new PartyCategory { PartyCategoryId = 4, Name= "Coop-demo", Selectable = true},
            new PartyCategory { PartyCategoryId = 5, Name= "Demo", Selectable = true},
            new PartyCategory { PartyCategoryId = 6, Name= "Fast Intro", Selectable = true},
            new PartyCategory { PartyCategoryId = 7, Name= "Fastcompo", Selectable = true},
            new PartyCategory { PartyCategoryId = 8, Name= "Game", Selectable = true},
            new PartyCategory { PartyCategoryId = 9, Name= "Graphics", Selectable = true},
            new PartyCategory { PartyCategoryId = 10, Name= "Intro", Selectable = true},
            new PartyCategory { PartyCategoryId = 11, Name= "Logo", Selectable = true},
            new PartyCategory { PartyCategoryId = 12, Name= "Mixed", Selectable = true},
            new PartyCategory { PartyCategoryId = 13, Name= "Music", Selectable = true},
            new PartyCategory { PartyCategoryId = 14, Name= "Oldschool Demo", Selectable = true},
            new PartyCategory { PartyCategoryId = 15, Name= "PC", Selectable = true},
            new PartyCategory { PartyCategoryId = 16, Name= "Realtime", Selectable = true},
            new PartyCategory { PartyCategoryId = 17, Name= "Wild", Selectable = true},
        };

        private int? PartyCategoryLookup(string category)
        {
            switch (category)
            {
                case "256b Intro":
                    return 1;

                case "4k":
                case "4k Intro":
                case "4kb Demo Comp.":
                case "4kb Intro":
                    return 2;

                case "Alternative Demo":
                    return 3;

                case "Coop-demo":
                    return 4;

                case "Demo":
                case "Democompo":
                    return 5;

                case "Fast Intro":
                case "Fast Intro Competition":
                    return 6;

                case "Fastcompo":
                    return 7;

                case "Game":
                    return 8;

                case "Gfx":
                case "Graphics":
                    return 9;

                case "Intro":
                    return 10;

                case "Logo":
                    return 11;

                case "Mixed":
                case "Mixed Demo":
                case "Mixed Demo Competiton":
                    return 12;

                case "Music":
                    return 13;

                case "Oldschool Demo":
                    return 14;

                case "PC-Compo":
                    return 15;

                case "Realtime Compo":
                    return 16;

                case "Wild":
                case "Wild Compo":
                case "Wild-Compo":
                    return 17;

                default:
                    return null;
            }
        }

        public async Task<bool> ImportVotes()
        {
            var oldVotes = _oldDbContext.OldVotes.OrderBy(p => p.Id);

            foreach (var oldVote in oldVotes)
            {
                string userId = null;
                if (oldVote.WriterId != null)
                {
                    var user = _dbContext.Users.FirstOrDefault(p => p.OldId == oldVote.WriterId);

                    if (user != null)
                        userId = user.Id;
                }
                else if (!string.IsNullOrEmpty(oldVote.Writer))
                {
                    var user = _dbContext.Users.FirstOrDefault(p => p.UserName == oldVote.Writer);
                    if (user != null)
                        userId = user.Id;
                }

                var production = await _unitOfWork.Productions.Get(oldVote.DemoId);

                if (production != null)
                {
                    if (oldVote.Vote > 0)
                    {
                        var vote = new Rating
                        {
                            ProductionId = oldVote.DemoId,
                            Value = oldVote.Vote,
                            Ip = oldVote.Ip,
                            UserId = userId,
                            RatedAt = oldVote.TimeStamp
                        };

                        _dbContext.Ratings.Add(vote);
                    }

                    if (!string.IsNullOrEmpty(oldVote.Remark))
                    {
                        var comment = new Comment
                        {
                            ProductionId = oldVote.DemoId,
                            Text = oldVote.Remark,
                            Ip = oldVote.Ip,
                            UserId = userId,
                            CommentedAt = oldVote.TimeStamp
                        };

                        _dbContext.Comments.Add(comment);
                    }

                    Console.Write(oldVote.Id + " / ");
                }
                else
                {
                    Debug.WriteLine("Production " + oldVote.DemoId + " not found?!");
                }
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SortImagesAsync()
        {
            var productions = _dbContext.Productions.Include(p => p.ProductionPictures);

            foreach (var production in productions)
            {
                var pictures = production.ProductionPictures.OrderBy(p => p.Filename);
                var cnt = 0;
                foreach (var picture in pictures)
                {
                    picture.Sort = cnt;
                    cnt++;
                }

                Console.Write(".");
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ImportDownloads()
        {
            //return Content("already done");
            //  ImportOld();
            //  return null;
            //  var downloads = _dbContext.OldDownloads.ToList();
            var demoFiles = _dbContext.ProductionFiles.ToList();

            for (var y = 2000; y <= DateTime.Now.Year; y++)
            {
                for (var m = 1; m < 13; m++) //1- 13
                {
                    Console.WriteLine("Import " + y + " / " + m);
                    await ImportMonth(y, m);
                }
            }

            Console.WriteLine("Thats all!");
            return true;
        }

        public async Task ImportOldDownloads()
        {
            //return;
            var demoFiles = _dbContext.ProductionFiles.ToList();
            string tableName;

            tableName = "Downloads";

            try
            {
                var oc = _serviceProvider.GetService<OldApplicationDbContext>();

                var downloads = oc.OldDownloadOlds.FromSqlRaw(string.Format("SELECT * FROM {0}", tableName));

                var cnt = 0;

                foreach (var download in downloads)
                {
                    cnt++;

                    if (cnt % 10000 == 0)
                        Console.Write(cnt + " ");

                    var dfId = demoFiles.FirstOrDefault(p => p.ProductionId == download.DemoId);

                    if (dfId != null)
                    {
                        var newDownload = new Download
                        {
                            ProductionFileId = dfId.ProductionFileId,
                            DownloadedOn = download.Datum,
                            Ip = download.IP
                        };

                        _dbContext.Downloads.Add(newDownload);
                    }
                }

                Console.WriteLine("Saving..." + cnt + " for Table " + tableName + ".....");
                await _dbContext.SaveChangesAsync();
                Debug.WriteLine("done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(tableName + " failed (" + e.Message + ")");
            }
        }

        private async Task ImportMonth(int year, int month)
        {
            //return;
            var demoFiles = _dbContext.ProductionFiles.ToList();
            string tableName;

            if (year == 0 && month == 0)
                tableName = "Downloads";
            else
                tableName = "Downloads_" + year + month.ToString("00");

            try
            {
                var oc = _serviceProvider.GetService<OldApplicationDbContext>();

                var downloads = oc.OldDownloads.FromSqlRaw(string.Format("SELECT * FROM {0}", tableName));

                var cnt = 0;

                foreach (var download in downloads)
                {
                    cnt++;

                    if (cnt % 10000 == 0)
                        Console.Write(cnt + " ");

                    if (download.Datum < new DateTime(year, month, 1))
                    {
                        Console.WriteLine($"Datum < Tabelle in Downloads Tabelle: {tableName} DemoId {download.DemoId} IP {download.IP} Datum: {download.Datum} ID {download.ID}");
                    }

                    var dfId = demoFiles.FirstOrDefault(p => p.ProductionId == download.DemoId);

                    if (dfId != null)
                    {
                        var newDownload = new Download
                        {
                            ProductionFileId = dfId.ProductionFileId,
                            DownloadedOn = download.Datum,
                            Ip = download.IP
                        };

                        _dbContext.Downloads.Add(newDownload);
                    }
                }

                Console.WriteLine("Saving..." + cnt + " for Table " + tableName + ".....");
                await _dbContext.SaveChangesAsync();
                Debug.WriteLine("done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(tableName + " failed (" + e.Message + ")");
            }
        }
    }
}