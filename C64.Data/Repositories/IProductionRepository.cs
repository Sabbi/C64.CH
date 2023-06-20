using C64.Data.Entities;
using C64.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IProductionRepository : IRepository<Production>
    {
        Task<bool> AddDownload(string filename, string remoteIp, string referer, string userId = null);

        void AddHistory(HistoryRecord historyProduction);

        Task<Production> GetDetails(int productionId);

        Task<IEnumerable<Download>> GetDownloads(int productionId, int numberOfMonths = 12);

        Task<ProductionFile> GetFile(int productionFileId);

        Task<IEnumerable<Production>> GetForScener(int scenerId);

        Task<IEnumerable<HistoryRecord>> GetHistory(int productionId);

        Task<Dictionary<string, int>> GetNumberOfReleasesPerLetter();

        Task<Dictionary<int, int>> GetNumberOfReleasesPerYear();

        Task<PaginatedResult<Production>> GetPaginatedWithGroups(Expression<Func<Production, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize);

        Task<ProductionFile> GetProductionFileByProductionId(int productionId);

        Task<Production> GetRandomProduction();

        Task<IEnumerable<Rating>> GetRatings(int productionId);

        Task<IEnumerable<int>> GetIdsOfGroupReleasesByLetter(string letter);

        Task<IEnumerable<ProductionFile>> ProductionFiles(int productionId);

        Task<IEnumerable<ProductionPicture>> ProductionPictures(int productionId);

        void UpdateProductionStats();

        void UpdateSingleProductionStat(Production production);
    }
}