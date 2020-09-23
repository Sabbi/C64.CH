using C64.Data.Entities;
using Newtonsoft.Json;

namespace C64.Data.History
{
    public class AddScenerApplier : IHistoryApplier
    {
        public void Apply(object scener, HistoryRecord historyProduction)
        {
            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var scener = (Scener)entity;
            var dbhistory = new HistoryRecord
            {
                AffectedScenerId = scener.ScenerId,
                AffectedEntity = historyentity,
                Property = "AddScener",
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = scener.GetType().FullName,
                Version = 1M,
                Description = $"Added scener '{scener.Handle}'"
            };

            return dbhistory;
        }
    }
}