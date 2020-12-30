using C64.Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace C64.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public IProductionRepository Productions { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public IPartyRepository Parties { get; private set; }
        public ISiteInfoRepository SiteInfos { get; private set; }
        public IGuestbookEntryRepository GuestbookEntries { get; private set; }
        public ILinkRepository Links { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public IScenerRepository Sceners { get; private set; }

        public IUserRepository Users { get; private set; }

        public IToolRepository Tools { get; private set; }
        public IStatisticRepository Statistics { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            this.context = context;
            this.logger = logger;

            Productions = new ProductionRepository(context, logger);
            Groups = new GroupRepository(context, logger);
            Parties = new PartyRepository(context, logger);
            SiteInfos = new SiteInfoRepository(context, logger);
            GuestbookEntries = new GuestbookEntryRepository(context, logger);
            Links = new LinkRepository(context, logger);
            Countries = new CountryRepository(context, logger);
            Sceners = new ScenerRepository(context, logger);
            Users = new UserRepository(context, logger);
            Tools = new ToolRepository(context, logger);
            Statistics = new StatisticRepository(context, logger);
        }

        public async Task<int> Commit()
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = await context.SaveChangesAsync();
            sw.Stop();
            logger.LogInformation("Commit result: {Result}, Time: {ElapsedMilliseconds}ms", result, sw.ElapsedMilliseconds);
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}