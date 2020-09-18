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
                    Apply((Production)entity, historyRecord);
                    break;

                case HistoryEntity.Group:
                    Apply((Group)entity, historyRecord);
                    break;

                case HistoryEntity.Party:
                    Apply((Party)entity, historyRecord);
                    break;

                default:
                    throw new NotImplementedException($"GenericHistoryApplier for {historyRecord.AffectedEntity} does not exist");
            }
        }

        public void Apply<T>(T entity, HistoryRecord historyRecord)
        {
            var type = Type.GetType(historyRecord.Type, true);

            var propertyName = FixPropertyName(historyRecord.Property);

            var property = typeof(T).GetProperty(propertyName);
            if (historyRecord.NewValue == null)
            {
                property.SetValue(entity, null);
            }
            else
            {
                var des = JsonConvert.DeserializeObject(historyRecord.NewValue, type);
                property.SetValue(entity, des);
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

                case HistoryEntity.Party:
                    historyRecord = CreateHistory(property, (Party)entity, newValue, status);
                    break;

                case HistoryEntity.Scener:
                    throw new NotImplementedException();
            }
            return historyRecord;
        }

        private HistoryRecord CreateHistory<T>(HistoryEditProperty property, T entity, object newValue, HistoryStatus status)
        {
            var propertyName = FixPropertyName(property.ToString());

            var propInfo = typeof(T).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(entity);

            var dbHistory = CreateBaseRecord(entity);

            dbHistory.Property = property.ToString();
            dbHistory.NewValue = newValue == null ? null : JsonConvert.SerializeObject(newValue);
            dbHistory.OldValue = oldValue == null ? null : JsonConvert.SerializeObject(oldValue);
            dbHistory.Status = status;
            dbHistory.Type = propInfo.PropertyType.FullName;
            dbHistory.Version = 1M;
            dbHistory.Description = GenerateHistoryDescription(property, entity, newValue);

            return dbHistory;
        }

        private string FixPropertyName(string property)
        {
            var propertyName = property.ToString();

            if (propertyName.EndsWith("Description"))
                return "Description";

            if (propertyName.StartsWith("Party") && !propertyName.Equals("Party"))
            {
                return propertyName.Substring(5);
            }

            return propertyName;
        }

        private HistoryRecord CreateBaseRecord(object entity)
        {
            HistoryEntity affectedEntity = HistoryEntity.Production;

            int? affectedProductionId = null;
            int? affectedGroupId = null;
            int? affectedPartyId = null;

            if (entity is Production)
            {
                affectedProductionId = (entity as Production).Id;
                affectedEntity = HistoryEntity.Production;
            }
            else if (entity is Group)
            {
                affectedGroupId = (entity as Group).Id;
                affectedEntity = HistoryEntity.Group;
            }
            else if (entity is Party)
            {
                affectedPartyId = (entity as Party).Id;
                affectedEntity = HistoryEntity.Party;
            }

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = affectedProductionId,
                AffectedGroupId = affectedGroupId,
                AffectedPartyId = affectedPartyId,
                AffectedEntity = affectedEntity,
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
                case HistoryEditProperty.PartyName:
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
                case HistoryEditProperty.PartyEmail:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Email removed";

                    return $"Email changed to '{newValue}'";

                case HistoryEditProperty.Url:
                case HistoryEditProperty.PartyUrl:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Url removed";

                    return $"Url changed to '{newValue}'";

                case HistoryEditProperty.GroupDescription:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Group description removed";

                    return $"Group description changed to '{newValue}'";

                case HistoryEditProperty.CountryId:
                case HistoryEditProperty.PartyCountryId:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Country removed";

                    return $"Country changed to {newValue}";

                case HistoryEditProperty.PartyDescription:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Party description removed";

                    return $"Party description changed to '{newValue}'";

                case HistoryEditProperty.PartyFrom:
                    return $"Party starting date changed from {production.From.ToShortDateString()} to {(newValue as DateTime?).GetValueOrDefault().ToShortDateString()}";

                case HistoryEditProperty.PartyTo:
                    return $"Party ending date changed from {production.To} to {(newValue as DateTime?).GetValueOrDefault().ToShortDateString()}";

                case HistoryEditProperty.PartyLocation:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Party location removed";

                    return $"Party location changed to '{newValue}'";

                case HistoryEditProperty.PartyOrganizers:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Party organizers removed";

                    return $"Party organizers changed to '{newValue}'";

                default:
                    throw new NotImplementedException($"Description for {property} lacks implementation");
            }
        }
    }
}