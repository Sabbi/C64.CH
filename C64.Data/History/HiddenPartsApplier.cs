using C64.Data.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class HiddenPartsApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValues = JsonConvert.DeserializeObject<Dictionary<int, string>>(historyProduction.NewValue);

            production.HiddenParts.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.HiddenParts.Add(new HiddenPart { HiddenPartId = newValue.Key, Description = newValue.Value });
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newParts = (List<HiddenPart>)newValue;

            var oldParts = production.HiddenParts;

            var newValues = new Dictionary<int, string>();
            var oldValues = new Dictionary<int, string>();

            foreach (var newPart in newParts.Where(p => p.Description != string.Empty))
                newValues.Add(newPart.HiddenPartId, newPart.Description);

            foreach (var oldPart in oldParts.Where(p => p.Description != string.Empty))
                oldValues.Add(oldPart.HiddenPartId, oldPart.Description);

            string description = null;

            if (oldParts.Any() && !newParts.Any())
                description = "Hiddenparts removed";
            else if (!oldParts.Any() && newParts.Any())
            {
                var newList = new List<string>();

                foreach (var part in newParts)
                {
                    var partString = part.Description.Length > 15 ? part.Description.Substring(0, 15) + "..." : part.Description;
                    newList.Add($"'{partString}'");
                }

                description = "Hiddenparts added: " + string.Join(", ", newList);
            }
            else if (oldParts.Count == newParts.Count)
            {
                var changed = false;

                for (int i = 0; i < oldParts.Count; i++)
                {
                    if (oldParts.ElementAt(i).Description != newParts.ElementAt(i).Description)
                        changed = true;
                }

                if (changed)
                    description = "Hiddenparts updated";
            }

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = historyEntity,
                Property = "HiddenParts",
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = newValues.GetType().FullName,
                Version = 1M,
                Description = description
            };

            return dbhistory;
        }
    }
}