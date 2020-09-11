using C64.Data.Entities;
using Newtonsoft.Json;
using System;

namespace C64.Data.History
{
    public class GenericHistoryApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyRecord)
        {
            switch (historyRecord.AffectedEntity)
            {
                case HistoryEntity.Production:
                    ApplyProduction((Production)entity, historyRecord);
                    break;

                case HistoryEntity.Group:
                    ApplyGroup((Group)entity, historyRecord);
                    break;

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
        }

        private void ApplyProduction(Production production, HistoryRecord historyRecord)
        {
            var type = Type.GetType(historyRecord.Type, true);
            var property = typeof(Production).GetProperty(historyRecord.Property);
            if (historyRecord.NewValue == null)
            {
                property.SetValue(production, null);
            }
            else
            {
                var des = JsonConvert.DeserializeObject(historyRecord.NewValue, type);
                property.SetValue(production, des);
            }
        }

        private void ApplyGroup(Group production, HistoryRecord historyRecord)
        {
            var type = Type.GetType(historyRecord.Type, true);

            var propertyName = historyRecord.Property;

            // Hack
            if (propertyName == "GroupDescription")
                propertyName = "Description";

            var property = typeof(Group).GetProperty(propertyName);
            if (historyRecord.NewValue == null)
            {
                property.SetValue(production, null);
            }
            else
            {
                var des = JsonConvert.DeserializeObject(historyRecord.NewValue, type);
                property.SetValue(production, des);
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            HistoryRecord historyRecord = null;

            switch (historyEntity)
            {
                case HistoryEntity.Production:
                    historyRecord = CreateProductionHistory(property, (Production)entity, newValue, status);
                    break;

                case HistoryEntity.Group:
                    historyRecord = CreateGroupHistory(property, (Group)entity, newValue, status);
                    break;

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
            return historyRecord;
        }

        private HistoryRecord CreateProductionHistory(HistoryEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            // Default, Simple Datatype properties
            var propertyName = property.ToString();

            var propInfo = typeof(Production).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(production);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                AffectedEntity = HistoryEntity.Production,
                Property = propertyName,
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue),
                OldValue = oldValue == null ? null : JsonConvert.SerializeObject(oldValue),
                Status = status,
                Type = propInfo.PropertyType.FullName,
                Version = 1M,
                Description = GenerateHistoryDescription(property, production, newValue)
            };
            return dbhistory;
        }

        private HistoryRecord CreateGroupHistory(HistoryEditProperty property, Group group, object newValue, HistoryStatus status)
        {
            // Default, Simple Datatype properties
            var propertyName = property.ToString();

            // Hack
            if (propertyName == "GroupDescription")
                propertyName = "Description";

            var propInfo = typeof(Group).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(group);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = group.GroupId,
                AffectedEntity = HistoryEntity.Group,
                Property = property.ToString(),
                NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue),
                OldValue = oldValue == null ? null : JsonConvert.SerializeObject(oldValue),
                Status = status,
                Type = propInfo.PropertyType.FullName,
                Version = 1M,
                Description = GenerateHistoryDescription(property, group, newValue)
            };
            return dbhistory;
        }

        public static string GenerateHistoryDescription(HistoryEditProperty property, dynamic production, object newValue, decimal version = 1M)
        {
            switch (property)
            {
                case HistoryEditProperty.Platform:
                    return $"Platform changed from '{production.Platform}' to '{newValue}'";

                case HistoryEditProperty.Name:
                    return $"Name changed from '{production.Name}' to '{newValue}'";

                case HistoryEditProperty.Aka:
                    if (string.IsNullOrEmpty(production.Aka))
                        return $"Aka set to '{newValue}'";
                    else if (string.IsNullOrEmpty(newValue.ToString()))
                        return $"Removed Aka '{production.Aka}'";
                    else
                        return $"Aka changed from '{production.Aka}' to '{newValue}'";

                case HistoryEditProperty.SubCategory:
                    return $"Subcategory changed from '{production.SubCategory}' to '{newValue}'";

                case HistoryEditProperty.Remarks:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Remark removed";

                    var display = newValue.ToString().Length > 15 ? newValue.ToString().Substring(0, 15) + "..." : newValue.ToString();
                    return $"Remark changed to '{display}'";

                case HistoryEditProperty.VideoType:
                    return $"Videotype changed from '{production.VideoType}' to '{newValue}'";

                case HistoryEditProperty.Email:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Email removed";

                    return $"Email changed to '{newValue}'";

                case HistoryEditProperty.Url:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Url removed";

                    return $"Url changed to '{newValue}'";

                case HistoryEditProperty.GroupDescription:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Group description removed";

                    return $"Group description changed to '{newValue}'";

                default:
                    throw new NotImplementedException($"Description for {property} lacks implementation");
            }
        }
    }
}