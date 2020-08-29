using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class ProductionCreditsApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValues = JsonConvert.DeserializeObject<List<EditCredit>>(historyProduction.NewValue);

            foreach (var newValue in newValues)
            {
                if (newValue.Deleted)
                {
                    var toDelete = production.ProductionCredits.FirstOrDefault(p => p.ProductionCreditId == newValue.Id);
                    production.ProductionCredits.Remove(toDelete);
                }
                else if (newValue.Added)
                    production.ProductionCredits.Add(new ProductionCredit { Credit = newValue.Credit, ScenerId = newValue.ScenerId });
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var oldValues = new List<EditCredit>();

            foreach (var productionCredit in production.ProductionCredits)
                oldValues.Add(new EditCredit { Id = productionCredit.ProductionCreditId, Added = false, Deleted = false, Credit = productionCredit.Credit, ScenerId = productionCredit.ScenerId, ScenerHandle = productionCredit.Scener.HandleWithGroups() });

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                Property = "ProductionCredits",
                NewValue = JsonConvert.SerializeObject(newValue),
                OldValue = JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M
            };

            return dbhistory;
        }
    }
}