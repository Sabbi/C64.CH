using C64.Data.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace C64.Data.History
{
    public class PartyApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
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

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newValues = (PartyApplierData)newValue;

            var oldValues = new PartyApplierData
            {
                PartyId = production.ProductionsParties.FirstOrDefault()?.PartyId ?? 0,
                CategoryId = production.ProductionsParties.FirstOrDefault()?.PartyCategoryId ?? null,
                Rank = production.ProductionsParties.FirstOrDefault()?.Rank ?? 0,
            };

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "Party",
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(PartyApplierData).FullName,
                Version = 1M
            };

            return dbhistory;
        }
    }

    public class PartyApplierData
    {
        public int PartyId { get; set; }
        public int Rank { get; set; }
        public int? CategoryId { get; set; }
    }
}