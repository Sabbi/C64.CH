using C64.Data.Entities;
using C64.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class SiteInfoRepository : Repository<SiteInfo>, ISiteInfoRepository
    {
        public SiteInfoRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public Task<PaginatedResult<SiteInfo>> GetPaginatedWithUser(Expression<Func<SiteInfo, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var query = context.Set<SiteInfo>().Include(p => p.User);
            return FindPaginated(query, predicate, orderBy, isSortedAscending, page, pageSize);
        }
    }
}