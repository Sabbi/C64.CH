using C64.Data.Entities;
using C64.Data.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedResult<HistoryRecord>> GetPaginatedHistory(Expression<Func<HistoryRecord, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize);
        Task<User> GetWithFavorites(string userId);
        Task<bool> IsFavorite(string userId, int productionId);
        void SetFavorite(string userId, int productionId, bool set);
    }
}