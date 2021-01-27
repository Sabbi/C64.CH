using C64.Data.Extensions;
using C64.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly ILogger logger;

        public Repository(DbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> Find<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int skip = 0, int take = 0)
        {
            var immediate = context.Set<T>().Where(predicate).OrderBy(orderBy);

            if (skip > 0)
                immediate = (IOrderedQueryable<T>)immediate.Skip(skip);

            if (take > 0)
                immediate = (IOrderedQueryable<T>)immediate.Take(take);

            return await immediate.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<T>> FindPaginated(IQueryable<T> query, Expression<Func<T, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var startRecord = (page - 1) * pageSize;
            IQueryable<T> results;
            results = query.OrderBy(orderBy, isSortedAscending).Where(predicate);

            var numberOfRecords = results.Count();

            var retVal = new PaginatedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalNumberOfRecords = numberOfRecords,
                Data = results.Skip(startRecord).Take(pageSize).ToList()
            };

            return Task.FromResult(retVal);
        }

        public Task<PaginatedResult<T>> FindPaginated(Expression<Func<T, bool>> predicate, string orderBy, bool isSortedAscending, int page, int pageSize)
        {
            var query = context.Set<T>();
            return FindPaginated(query, predicate, orderBy, isSortedAscending, page, pageSize);
        }
    }
}