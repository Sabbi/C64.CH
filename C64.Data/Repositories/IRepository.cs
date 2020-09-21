using C64.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        //Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> orderBy, int skip = 0, int take = 0);
        Task<IEnumerable<T>> Find<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int skip = 0, int take = 0);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        Task<PaginatedResult<T>> FindPaginated(Expression<Func<T, bool>> predicate, string orderBy, bool isSortedAscending, int page = 1, int pageSize = 10);

        Task<PaginatedResult<T>> FindPaginated(IQueryable<T> query, Expression<Func<T, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize);
    }
}