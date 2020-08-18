using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public class PartyRepository : Repository<Party>, IPartyRepository
    {
        public PartyRepository(DbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<ICollection<PartyCategory>> GetCategories()
        {
            return await context.Set<PartyCategory>().ToListAsync();
        }

        public Task<Party> GetWithProductions(int partyId)
        {
            return context.Set<Party>().Include(p => p.ProductionsParties).ThenInclude(p => p.Production).FirstOrDefaultAsync(p => p.PartyId == partyId);
        }
    }
}