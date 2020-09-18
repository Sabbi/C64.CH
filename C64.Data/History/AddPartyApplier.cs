using C64.Data.Entities;
using Newtonsoft.Json;

namespace C64.Data.History
{
    public class AddPartyApplier : IHistoryApplier
    {
        public void Apply(object production, HistoryRecord historyProduction)
        {
            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var party = (Party)entity;
            var dbhistory = new HistoryRecord
            {
                AffectedPartyId = party.PartyId,
                AffectedEntity = historyentity,
                Property = "AddParty",
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = party.GetType().FullName,
                Version = 1M,
                Description = $"Added party '{party.Name}'"
            };

            return dbhistory;
        }
    }
}