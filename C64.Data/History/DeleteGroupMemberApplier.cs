using C64.Data.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace C64.Data.History
{
    public class DeleteGroupMemberApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var group = (Group)entity;
            var toDelete = JsonConvert.DeserializeObject<AddGroupMember>(historyProduction.OldValue);

            var toDeleteLinked = group.ScenerGroups.FirstOrDefault(p => p.ScenerId == toDelete.Scener.ScenerId);
            group.ScenerGroups.Remove(toDeleteLinked);

            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;
            var toDelete = (int)newValue;

            var oldMemberData = group.ScenerGroups.FirstOrDefault(p => p.ScenerId == toDelete);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = group.GroupId,
                AffectedEntity = historyentity,
                Property = "DeleteGroupMember",
                NewValue = null,
                OldValue = JsonConvert.SerializeObject(oldMemberData, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                Status = status,
                Type = group.GetType().FullName,
                Version = 1M,
                Description = $"Member '{oldMemberData.Scener.Handle}' deleted from group '{group.Name}'"
            };

            return dbhistory;
        }
    }
}