using C64.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace C64.Data.History
{
    public class AddGroupMemberApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var group = (Group)entity;
            var newMember = JsonConvert.DeserializeObject<AddGroupMember>(historyProduction.NewValue);

            var newScenersGroups = new ScenersGroups
            {
                ScenerId = newMember.Scener.ScenerId,
                GroupId = group.GroupId,
                ValidFrom = newMember.JoinedDate,
                ValidFromType = newMember.JoinedDateType,
                ValidTo = newMember.LeftDate,
                ValidToType = newMember.LeftDateType
            };

            foreach (var job in newMember.SelectedJobs)
                newScenersGroups.ScenerGroupJobs.Add(new ScenerGroupJob { Job = job });

            group.ScenersGroups.Add(newScenersGroups);
            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;
            var newMember = (AddGroupMember)newValue;

            if (newMember.LeftDateType == DateType.None)
                newMember.LeftDate = DateTime.MaxValue;

            newMember.Scener.ProductionsSceners = new HashSet<ProductionsSceners>();
            newMember.Scener.ScenersGroups = new HashSet<ScenersGroups>();
            newMember.Scener.PartiesSceners = new HashSet<PartiesSceners>();
            newMember.Scener.AlterEgos = new HashSet<ScenersSceners>();

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = group.GroupId,
                AffectedScenerId = newMember.Scener.ScenerId,
                AffectedEntity = historyentity,
                Property = "AddGroupMember",
                NewValue = JsonConvert.SerializeObject(newMember, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = null,
                Status = status,
                Type = group.GetType().FullName,
                Version = 1M,
                Description = $"Member '{newMember.Scener.Handle}' added to group '{group.Name}'"
            };

            return dbhistory;
        }
    }
}