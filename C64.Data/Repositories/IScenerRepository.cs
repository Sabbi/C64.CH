using C64.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IScenerRepository : IRepository<Scener>
    {
        Task<IEnumerable<Scener>> FindWithGroups(Expression<Func<Scener, bool>> predicate);
        Task<Scener> GetDetails(int scenerId);
        Task<IEnumerable<HistoryRecord>> GetHistory(int scenerId);
        Task<Scener> GetWithProductions(int scenerId);
    }
}