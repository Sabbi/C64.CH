using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class MemberJobsApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var scenersGroups = ((Group)entity).ScenersGroups;

            var newValues = JsonConvert.DeserializeObject<IEnumerable<Job>>(historyProduction.NewValue);

            var affected = scenersGroups.FirstOrDefault(p => p.ScenerId == historyProduction.AffectedScenerId);

            affected.ScenerGroupJobs.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
            {
                affected.ScenerGroupJobs.Add(new ScenerGroupJob { Job = newValue });
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;

            var newValues = (AddGroupMember)newValue;

            var oldValues = group.ScenersGroups.FirstOrDefault(p => p.ScenerId == newValues.Scener.ScenerId).ScenerGroupJobs.Select(p => p.Job);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = null,
                AffectedGroupId = group.GroupId,
                AffectedScenerId = newValues.Scener.ScenerId,
                AffectedEntity = historyEntity,
                Property = "MemberJobs",
                NewValue = newValues.SelectedJobs == null ? null : JsonConvert.SerializeObject(newValues.SelectedJobs),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(IEnumerable<int>).FullName,
                Version = 1M,
                Description = $"Member jobs of '{group.ScenersGroups.FirstOrDefault().Scener.Handle}' changed to '{string.Join(", ", newValues.SelectedJobs)}'"
            };

            return dbhistory;
        }
    }
}