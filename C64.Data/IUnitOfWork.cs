using C64.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace C64.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IProductionRepository Productions { get; }
        IGroupRepository Groups { get; }

        IPartyRepository Parties { get; }

        ISiteInfoRepository SiteInfos { get; }
        IGuestbookEntryRepository GuestbookEntries { get; }
        ILinkRepository Links { get; }

        ICountryRepository Countries { get; }

        IScenerRepository Sceners { get; }

        IUserRepository Users { get; }

        IToolRepository Tools { get; }

        IChangeLogRepository ChangeLogs { get; }

        IStatisticRepository Statistics { get; }

        Task<int> Commit();
    }
}