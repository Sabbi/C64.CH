using C64.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Party> GetDetails(int partyId, bool includeGroupData)
        {
            if (includeGroupData)
                return context.Set<Party>()
                    .Include(p => p.Country)
                    .Include(p => p.ProductionsParties).ThenInclude(p => p.PartyCategory)
                    .Include(p => p.ProductionsParties).ThenInclude(p => p.Production).ThenInclude(p => p.ProductionPictures)
                    .Include(p => p.ProductionsParties).ThenInclude(p => p.Production).ThenInclude(p => p.ProductionsGroups).ThenInclude(p => p.Group)
                    .FirstOrDefaultAsync(p => p.PartyId == partyId);

            return context.Set<Party>().Include(p => p.Country).Include(p => p.ProductionsParties).ThenInclude(p => p.Production).FirstOrDefaultAsync(p => p.PartyId == partyId);
        }

        public Task<Party> GetWithProductions(int partyId)
        {
            return context.Set<Party>().Include(p => p.ProductionsParties).ThenInclude(p => p.Production).FirstOrDefaultAsync(p => p.PartyId == partyId);
        }

        public async Task<IEnumerable<HistoryRecord>> GetHistory(int partyId)
        {
            return await context.Set<HistoryRecord>().Where(p => p.AffectedPartyId == partyId).Include(p => p.User).Include(p => p.AffectedParty).ToListAsync();
        }
    }
}