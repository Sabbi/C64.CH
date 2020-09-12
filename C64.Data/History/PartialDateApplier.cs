using C64.Data.Entities;
using C64.Data.Extensions;
using Newtonsoft.Json;
using System;

namespace C64.Data.History
{
    public class PartialDateApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            switch (historyProduction.AffectedEntity)
            {
                case HistoryEntity.Production:
                    Apply((Production)entity, historyProduction);
                    break;

                case HistoryEntity.Group:
                    Apply((Group)entity, historyProduction);
                    break;

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            HistoryRecord historyRecord = null;

            switch (historyEntity)
            {
                case HistoryEntity.Production:
                    historyRecord = CreateHistory(property, (Production)entity, newValue, status);
                    break;

                case HistoryEntity.Group:
                    historyRecord = CreateHistory(property, (Group)entity, newValue, status);
                    break;

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
            return historyRecord;
        }

        private void Apply<T>(T entity, HistoryRecord historyRecord)
        {
            var newValues = JsonConvert.DeserializeObject<PartialDateApplierData>(historyRecord.NewValue);

            var property = typeof(T).GetProperty(historyRecord.Property);
            property.SetValue(entity, newValues.Date);

            var property2 = typeof(T).GetProperty(historyRecord.Property + "Type");
            property2.SetValue(entity, newValues.Type);
        }

        public HistoryRecord CreateHistory<T>(HistoryEditProperty property, T production, object newValue, HistoryStatus status)
        {
            var newValues = (PartialDateApplierData)newValue;

            var oldValues = new PartialDateApplierData();
            var propertyName = property.ToString();

            var propInfo = typeof(T).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(production);

            oldValues.Date = (DateTime)oldValue;

            var propInfo2 = typeof(T).GetProperty(propertyName + "Type");
            var oldValue2 = propInfo2.GetValue(production);

            oldValues.Type = (DateType)oldValue2;

            string description;
            var dateName = DateName(property);
            if (newValues.Type == DateType.None)
                description = $"{dateName} removed";
            else if (oldValues.Type == DateType.None)
                description = $"{dateName} set to '{newValues.Date.ParseDate(newValues.Type)}'";
            else
                description = $"{dateName} changed from '{oldValues.Date.ParseDate(oldValues.Type)}' to '{newValues.Date.ParseDate(newValues.Type)}'";

            int? affectedProductionId = null;
            int? affectedGroupId = null;

            HistoryEntity affectedEntity = HistoryEntity.Production;

            if (production is Production)
            {
                affectedProductionId = (production as Production).Id;
                affectedEntity = HistoryEntity.Production;
            }
            else if (production is Group)
            {
                affectedGroupId = (production as Group).Id;
                affectedEntity = HistoryEntity.Group;
            }

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = affectedProductionId,
                AffectedGroupId = affectedGroupId,
                AffectedEntity = affectedEntity,
                Property = propertyName,
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValue == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(PartialDateApplierData).FullName,
                Version = 1M,
                Description = description
            };
            return dbhistory;
        }

        private string DateName(HistoryEditProperty property)
        {
            switch (property)
            {
                case HistoryEditProperty.ReleaseDate:
                    return "Release date";

                case HistoryEditProperty.FoundedDate:
                    return "Founded date";

                case HistoryEditProperty.ClosedDate:
                    return "Closed date";
            }

            throw new NotImplementedException($"Not implemented DateName {property}");
        }
    }

    public class PartialDateApplierData
    {
        public DateTime Date { get; set; }
        public DateType Type { get; set; }
    }
}