using C64.Data.Entities;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IPartyRepository : IRepository<Party>
    {
        Task<Party> GetWithProductions(int partyId);
    }
}