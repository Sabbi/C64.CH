using C64.Data.Entities;
using C64.Data.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface ISiteInfoRepository : IRepository<SiteInfo>
    {
        Task<PaginatedResult<SiteInfo>> GetPaginatedWithUser(Expression<Func<SiteInfo, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize);
    }
}