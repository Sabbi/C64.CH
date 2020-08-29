using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class GroupsApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;

            var newValues = JsonConvert.DeserializeObject<IEnumerable<int>>(historyProduction.NewValue);

            production.ProductionsGroups.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
            {
                production.ProductionsGroups.Add(new ProductionsGroups { GroupId = newValue });
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newValues = (IList<Group>)newValue;

            var toStore = newValues.Select(p => p.GroupId);

            var oldValues = production.ProductionsGroups.Select(p => p.GroupId);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = historyEntity,
                Property = "Groups",
                NewValue = toStore == null ? null : JsonConvert.SerializeObject(toStore),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(IEnumerable<int>).FullName,
                Version = 1M
            };

            return dbhistory;
        }
    }
}