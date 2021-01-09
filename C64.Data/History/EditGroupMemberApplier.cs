using C64.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class EditGroupMemberApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var group = (Group)entity;
            var newMember = JsonConvert.DeserializeObject<AddGroupMember>(historyProduction.NewValue);

            var affectedMember = group.ScenersGroups.FirstOrDefault(p => p.ScenerId == newMember.Scener.ScenerId);

            affectedMember.ValidFrom = newMember.JoinedDate;
            affectedMember.ValidFromType = newMember.JoinedDateType;
            affectedMember.ValidTo = newMember.LeftDate;
            affectedMember.ValidToType = newMember.LeftDateType;

            affectedMember.ScenerGroupJobs.Clear();

            foreach (var job in newMember.SelectedJobs)
                affectedMember.ScenerGroupJobs.Add(new ScenerGroupJob { Job = job });

            return;
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyentity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;
            var newMemberData = (AddGroupMember)newValue;

            if (newMemberData.LeftDateType == DateType.None)
                newMemberData.LeftDate = DateTime.MaxValue;

            newMemberData.Scener.ProductionsSceners = new HashSet<ProductionsSceners>();
            newMemberData.Scener.ScenersGroups = new HashSet<ScenersGroups>();
            newMemberData.Scener.PartiesSceners = new HashSet<PartiesSceners>();
            newMemberData.Scener.AlterEgos = new HashSet<ScenersSceners>();

            var oldMemberData = group.ScenersGroups.FirstOrDefault(p => p.ScenerId == newMemberData.Scener.ScenerId);

            oldMemberData.Scener.ProductionsSceners = new HashSet<ProductionsSceners>();
            oldMemberData.Scener.ScenersGroups = new HashSet<ScenersGroups>();
            oldMemberData.Scener.PartiesSceners = new HashSet<PartiesSceners>();
            oldMemberData.Scener.AlterEgos = new HashSet<ScenersSceners>();

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = group.GroupId,
                AffectedEntity = historyentity,
                Property = "EditGroupMember",
                NewValue = JsonConvert.SerializeObject(newMemberData, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                OldValue = JsonConvert.SerializeObject(oldMemberData, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                Status = status,
                Type = group.GetType().FullName,
                Version = 1M,
                Description = $"Member '{newMemberData.Scener.Handle}' edited"
            };

            return dbhistory;
        }
    }
}