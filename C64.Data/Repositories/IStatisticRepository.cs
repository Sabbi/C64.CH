using C64.Data.Entities;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IStatisticRepository : IRepository<Statistic>
    {
        Task<Statistic> Get();

        Task UpdateStats();
    }
}