using C64.Data.Entities;
using C64.Data.Models;
using Newtonsoft.Json;

namespace C64.Data.History
{
    public class AddProductionApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            return;
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "AddProduction",
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = production.GetType().FullName,
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