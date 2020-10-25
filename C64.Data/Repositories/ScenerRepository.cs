using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.
    Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class ScenerRepository : Repository<Scener>, IScenerRepository
    {
        public ScenerRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Scener>> FindWithGroups(Expression<Func<Scener, bool>> predicate)
        {
            var sceners = await context.Set<Scener>()
               .Include(p => p.ScenersGroups).ThenInclude(p => p.Group)
               .Where(predicate).ToListAsync();

            return sceners;
        }

        public async Task<Scener> GetDetails(int scenerId)
        {
            var scener = await context.Set<Scener>()
                .Include(p => p.ScenersGroups).ThenInclude(p => p.Group)
                .Include(p => p.ScenersGroups).ThenInclude(p => p.ScenerGroupJobs)
                .Include(p => p.Jobs)
                .Include(p => p.Country).FirstOrDefaultAsync(p => p.ScenerId == scenerId);

            return scener;
        }

        public async Task<IEnumerable<HistoryRecord>> GetHistory(int scenerId)
        {
            return await context.Set<HistoryRecord>().Where(p => p.AffectedScenerId == scenerId).Include(p => p.User).Include(p => p.AffectedScener).ToListAsync();
        }
    }
}