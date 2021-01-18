using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C64.Data.History
{
    public class ScenersApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;

            var newValues = JsonConvert.DeserializeObject<Dictionary<int, string>>(historyProduction.NewValue);

            production.ProductionsSceners.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
            {
                production.ProductionsSceners.Add(new ProductionsSceners { ScenerId = newValue.Key });
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newValues = (IList<Scener>)newValue;

            var toStore = newValues.ToDictionary(p => p.ScenerId, p => p.Handle);

            var oldValues = production.ProductionsSceners.ToDictionary(p => p.ScenerId, p => p.Scener.Handle);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = historyEntity,
                Property = "Sceners",
                NewValue = toStore == null ? null : JsonConvert.SerializeObject(toStore),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(IEnumerable<int>).FullName,
                Version = 1M,
                Description = CreateDescription(oldValues, toStore)
            };

            return dbhistory;
        }

        private string CreateDescription(Dictionary<int, string> oldValues, Dictionary<int, string> newValues)
        {
            var sbAdded = new StringBuilder();
            var sbRemoved = new StringBuilder();

            foreach (var newValue in newValues)
            {
                if (!oldValues.Contains(newValue))
                    sbAdded.Append(newValue.Value + ", ");
            }

            if (sbAdded.Length > 0)
            {
                sbAdded.Insert(0, "Added sceners: ");
                sbAdded.Remove(sbAdded.Length - 2, 2);
            }

            // removed -> wert nur in oldvalues drin
            foreach (var oldValue in oldValues)
            {
                if (!newValues.Contains(oldValue))
                    sbRemoved.Append(oldValue.Value + ", ");
            }

            if (sbRemoved.Length > 0)
            {
                sbRemoved.Insert(0, "Removed sceners: ");
                sbRemoved.Remove(sbRemoved.Length - 2, 2);
            }

            if (sbAdded.Length > 0 && sbRemoved.Length > 0)
                return sbAdded.ToString() + ", " + sbRemoved.ToString();

            return sbAdded.ToString() + sbRemoved.ToString();
        }
    }
}