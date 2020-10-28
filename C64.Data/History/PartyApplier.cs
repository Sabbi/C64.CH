using C64.Data.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace C64.Data.History
{
    public class PartyApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValue = JsonConvert.DeserializeObject<PartyApplierData>(historyProduction.NewValue);

            if (newValue == null || newValue.PartyId == 0)
            {
                production.ProductionsParties.Clear();
                return;
            }

            var prodParty = production.ProductionsParties.FirstOrDefault();

            if (prodParty != null)
            {
                prodParty.PartyId = newValue.PartyId;
                prodParty.PartyCategoryId = newValue.CategoryId;
                prodParty.Rank = newValue.Rank;
            }
            else
                production.ProductionsParties.Add(new ProductionsParties { PartyId = newValue.PartyId, PartyCategoryId = newValue.CategoryId, Rank = newValue.Rank });
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newValues = (PartyApplierData)newValue;

            var oldValues = new PartyApplierData
            {
                PartyId = production.ProductionsParties.FirstOrDefault()?.PartyId ?? 0,
                CategoryId = production.ProductionsParties.FirstOrDefault()?.PartyCategoryId ?? null,
                PartyName = production.ProductionsParties.FirstOrDefault()?.Party?.Name,
                CategoryName = production.ProductionsParties.FirstOrDefault()?.PartyCategory?.Name,
                Rank = production.ProductionsParties.FirstOrDefault()?.Rank ?? 0,
            };

            // Old and new
            string description = null;
            if (production.ProductionsParties.Any() && newValues.PartyId > 0)
                description = $"Partyinformation changed from '{production.ProductionsParties.FirstOrDefault().Party.Name}/{production.ProductionsParties.FirstOrDefault().PartyCategory?.Name}/{production.ProductionsParties.FirstOrDefault().Rank}' to '{newValues.PartyName}/{newValues?.CategoryName}/{newValues.Rank}'";
            else if (!production.ProductionsParties.Any() && newValues.PartyId > 0)
                description = $"Partyinformation added: '{newValues.PartyName}/{newValues?.CategoryName}/{newValues.Rank}'";
            else if (production.ProductionsParties.Any() && newValues.PartyId == 0)
                description = "Partyinformation removed";

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = historyEntity,
                Property = "Party",
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(PartyApplierData).FullName,
                Version = 1M,
                Description = description
            };

            return dbhistory;
        }
    }

    public class PartyApplierData
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int Rank { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}