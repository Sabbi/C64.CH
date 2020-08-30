using C64.Data.Entities;
using Newtonsoft.Json;

namespace C64.Data.History
{
    public class AddGroupApplier : IHistoryApplier
    {
        public void Apply(object production, HistoryRecord historyProduction)
        {
            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;
            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = group.GroupId,
                AffectedEntity = historyentity,
                Property = "AddGroup",
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = group.GetType().FullName,
                Version = 1M,
                Description = $"Added '{group.Name}'"
            };

            return dbhistory;
        }
    }
}