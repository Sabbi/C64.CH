using C64.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetDetails(int groupId);
        Task<IEnumerable<HistoryRecord>> GetHistory(int groupId);
        Task<Group> GetWithProductions(int groupId);

        void UpdateGroupStats();

        void UpdateSingleGroupStat(Group group);
    }
}