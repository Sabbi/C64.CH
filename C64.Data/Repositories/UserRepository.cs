using C64.Data.Entities;
using C64.Data.Extensions;
using C64.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext;

        public Task<User> GetWithFavorites(string userId)
        {
            return context.Set<User>().Include(p => p.Country).Include(p => p.Favorites).FirstOrDefaultAsync(p => p.Id == userId);
        }

        public Task<bool> IsFavorite(string userId, int productionId)
        {
            return context.Set<UserFavorite>().AnyAsync(p => p.ProductionId == productionId && p.UserId == userId);
        }

        public void SetFavorite(string userId, int productionId, bool set)
        {
            if (!set)
            {
                var favorite = context.Set<UserFavorite>().Where(p => p.UserId == userId && p.ProductionId == productionId);
                context.RemoveRange(favorite);
                return;
            }
            context.Set<UserFavorite>().Add(new UserFavorite { ProductionId = productionId, UserId = userId, Added = DateTime.Now });
        }

        public async Task<PaginatedResult<HistoryRecord>> GetPaginatedHistory(Expression<Func<HistoryRecord, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var query = context.Set<HistoryRecord>().Include(p => p.AffectedGroup).Include(p => p.AffectedProduction).Include(p => p.AffectedParty).Include(p => p.AffectedScener).Include(p => p.User);
            return await FindPaginated(query, predicate, orderBy, isSortedAscending, page, pageSize);
        }

        // TODO:  D(o)RY. Factor out
        private Task<PaginatedResult<HistoryRecord>> FindPaginated(IQueryable<HistoryRecord> query, Expression<Func<HistoryRecord, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var startRecord = (page - 1) * pageSize;
            IQueryable<HistoryRecord> results;
            results = query.OrderBy(orderBy, isSortedAscending).Where(predicate);

            var numberOfRecords = results.Count();

            var retVal = new PaginatedResult<HistoryRecord>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalNumberOfRecords = numberOfRecords,
                Data = results.Skip(startRecord).Take(pageSize).ToList()
            };

            return Task.FromResult(retVal);
        }
    }
}