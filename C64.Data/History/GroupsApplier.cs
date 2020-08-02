using C64.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class GroupsApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<IEnumerable<int>>(historyProduction.NewValue);

            production.ProductionsGroups.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
            {
                production.ProductionsGroups.Add(new ProductionsGroups { GroupId = newValue });
            }
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newValues = (IList<Group>)newValue;

            var toStore = newValues.Select(p => p.GroupId);

            var oldValues = production.ProductionsGroups.Select(p => p.GroupId);

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "Groups",
                NewValue = toStore == null ? null : JsonConvert.SerializeObject(toStore),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(IEnumerable<int>).FullName
            };

            return dbhistory;
        }

        public HistoryProduction CreateHistoryProduction(string property, Production production, object newValue, HistoryStatus status)
        {
            var editProperty = (ProductionEditProperty)Enum.Parse(typeof(ProductionEditProperty), property);
            return CreateHistoryProduction(editProperty, production, newValue, status);
        }
    }
}