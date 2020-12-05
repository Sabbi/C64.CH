using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                oldValues.Add(new EditCredit { Id = productionCredit.ProductionCreditId, Added = false, Deleted = false, Credit = productionCredit.Credit, ScenerId = productionCredit.ScenerId, ScenerHandle = productionCredit.Scener.Handle });

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                Property = "ProductionCredits",
                NewValue = JsonConvert.SerializeObject(newValue),
                OldValue = JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M,
                Description = CreateDescription((List<EditCredit>)newValue, production.Name)
            };

            return dbhistory;
        }

        private string CreateDescription(List<EditCredit> newValue, string prodName)
        {
            var sb = new StringBuilder();

            if (newValue.Any(p => p.Deleted))
            {
                sb.Append("Removed credits for ");

                foreach (var deleted in newValue.Where(p => p.Deleted))
                    sb.Append($"{deleted.ScenerHandle} ({deleted.Credit}), ");

                if (!newValue.Any(p => p.Added))
                    sb.Remove(sb.Length - 2, 2);
            }

            if (newValue.Any(p => p.Added))
            {
                sb.Append("Added credits for ");

                foreach (var added in newValue.Where(p => p.Added))
                    sb.Append($"{added.ScenerHandle} ({added.Credit}), ");

                sb.Remove(sb.Length - 2, 2);
            }

            if (newValue.Any(p => p.Deleted) && newValue.Any(predicate => predicate.Added))
                sb.Append(" to ");
            else if (newValue.Any(p => p.Deleted))
                sb.Append(" from ");
            else if (newValue.Any(p => p.Added))
                sb.Append(" to ");

            sb.Append(prodName);

            return sb.ToString();
        }
    }
}