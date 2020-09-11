using C64.Data.Entities;
using C64.Data.Extensions;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace C64.Data.History
{
    public class MembersPartialDateApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyRecord)
        {
            var group = (Group)entity;
            var newValues = JsonConvert.DeserializeObject<PartialDateApplierData>(historyRecord.NewValue);

            if (historyRecord.Property == "JoinedDate")
            {
                group.ScenerGroups.FirstOrDefault(p => p.ScenerId == historyRecord.AffectedScenerId).ValidFrom = newValues.Date;
                group.ScenerGroups.FirstOrDefault(p => p.ScenerId == historyRecord.AffectedScenerId).ValidFromType = newValues.Type;
            }
            else
            {
                group.ScenerGroups.FirstOrDefault(p => p.ScenerId == historyRecord.AffectedScenerId).ValidTo = newValues.Date;
                group.ScenerGroups.FirstOrDefault(p => p.ScenerId == historyRecord.AffectedScenerId).ValidToType = newValues.Type;
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var group = (Group)entity;

            var newValues = (AddGroupMember)newValue;

            var oldValues = group.ScenerGroups.FirstOrDefault(p => p.ScenerId == newValues.Scener.ScenerId);

            PartialDateApplierData defNewValues, defOldValues;

            if (property == HistoryEditProperty.JoinedDate)
            {
                defNewValues = new PartialDateApplierData { Date = newValues.JoinedDate, Type = newValues.JoinedDateType };
                defOldValues = new PartialDateApplierData { Date = oldValues.ValidFrom, Type = oldValues.ValidFromType };
            }
            else
            {
                defNewValues = new PartialDateApplierData { Date = newValues.LeftDate, Type = newValues.LeftDateType };
                defOldValues = new PartialDateApplierData { Date = oldValues.ValidTo, Type = oldValues.ValidToType };
            }

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = null,
                AffectedGroupId = group.GroupId,
                AffectedScenerId = newValues.Scener.ScenerId,
                AffectedEntity = historyEntity,
                Property = property.ToString(),
                NewValue = JsonConvert.SerializeObject(defNewValues),
                OldValue = JsonConvert.SerializeObject(defOldValues),
                Status = status,
                Type = typeof(PartialDateApplier).FullName,
                Version = 1M,
                Description = $"'{DateName(property)}' changed from '{defOldValues.Date.ParseDate(defOldValues.Type)}' to '{defNewValues.Date.ParseDate(defNewValues.Type)}'"
            };

            return dbhistory;
        }

        private string DateName(HistoryEditProperty property)
        {
            switch (property)
            {
                case HistoryEditProperty.JoinedDate:
                    return "Joined date";

                case HistoryEditProperty.LeftDate:
                    return "Left date";
            }
            throw new NotImplementedException();
        }
    }
}