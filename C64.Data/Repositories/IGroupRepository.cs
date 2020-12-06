using C64.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> FindWithSceners(Expression<Func<Group, bool>> predicate);
        Task<Group> GetDetails(int groupId);
        Task<IEnumerable<HistoryRecord>> GetHistory(int groupId);
        Task<Group> GetWithProductions(int groupId);

        void UpdateGroupStats();

        void UpdateSingleGroupStat(Group group);
    }
}