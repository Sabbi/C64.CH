﻿using C64.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C64.Data.Repositories
{
    public interface IPartyRepository : IRepository<Party>
    {
        Task<Party> GetWithProductions(int partyId);

        Task<ICollection<PartyCategory>> GetCategories();

        Task<IEnumerable<HistoryRecord>> GetHistory(int partyId);

        Task<Party> GetDetails(int partyId, bool includeGroupData = false);
    }
}