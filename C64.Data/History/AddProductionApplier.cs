using C64.Data.Entities;
using Newtonsoft.Json;

namespace C64.Data.History
{
    public class AddProductionApplier : IHistoryApplier
    {
        public void Apply(object production, HistoryRecord historyProduction)
        {
            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = historyentity,
                Property = "AddProduction",
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = production.GetType().FullName,
                Version = 1M,
                Description = $"Added '{production.Name}', a new {production.SubCategory}"
            };

            return dbhistory;
        }
    }
}