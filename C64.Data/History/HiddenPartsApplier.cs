using C64.Data.Entities;
using C64.Data.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class HiddenPartsApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<Dictionary<int, string>>(historyProduction.NewValue);

            production.HiddenParts.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.HiddenParts.Add(new HiddenPart { HiddenPartId = newValue.Key, Description = newValue.Value });
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newParts = (List<HiddenPart>)newValue;

            var oldParts = production.HiddenParts;

            var newValues = new Dictionary<int, string>();
            var oldValues = new Dictionary<int, string>();

            foreach (var newPart in newParts.Where(p => p.Description != string.Empty))
                newValues.Add(newPart.HiddenPartId, newPart.Description);

            foreach (var oldPart in oldParts.Where(p => p.Description != string.Empty))
                oldValues.Add(oldPart.HiddenPartId, oldPart.Description);

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "HiddenParts",
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = newValues.GetType().FullName,
                Version = 1M
            };

            return dbhistory;
        }

        public HistoryUndoResult Undo(Production production, HistoryProduction historyProduction, bool force = true)
        {
            throw new System.NotImplementedException();
        }
    }
}