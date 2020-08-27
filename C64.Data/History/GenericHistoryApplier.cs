using C64.Data.Entities;
using Newtonsoft.Json;
using System;

namespace C64.Data.History
{
    public class GenericHistoryApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var type = Type.GetType(historyProduction.Type, true);
            var property = typeof(Production).GetProperty(historyProduction.Property);
            if (historyProduction.NewValue == null)
            {
                property.SetValue(production, null);
            }
            else
            {
                var des = JsonConvert.DeserializeObject(historyProduction.NewValue, type);
                property.SetValue(production, des);
            }
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            // Default, Simple Datatype properties
            var propertyName = property.ToString();

            var propInfo = typeof(Production).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(production);

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
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

        public static string GenerateHistoryDescription(ProductionEditProperty property, Production production, object newValue, decimal version = 1M)
        {
            switch (property)
            {
                case ProductionEditProperty.Platform:
                    return $"Platform changed from '{production.Platform}' to '{newValue}'";

                case ProductionEditProperty.Name:
                    return $"Name changed from '{production.Name}' to '{newValue}'";

                case ProductionEditProperty.Aka:
                    if (string.IsNullOrEmpty(production.Aka))
                        return $"Aka set to '{newValue}'";
                    else if (string.IsNullOrEmpty(newValue.ToString()))
                        return $"Removed Aka '{production.Aka}'";
                    else
                        return $"Aka changed from '{production.Aka}' to '{newValue}'";

                case ProductionEditProperty.SubCategory:
                    return $"Subcategory changed from '{production.SubCategory}' to '{newValue}'";

                case ProductionEditProperty.Remarks:
                    if (newValue == null || string.IsNullOrEmpty(newValue.ToString()))
                        return "Remark removed";

                    var display = newValue.ToString().Length > 15 ? newValue.ToString().Substring(0, 15) + "..." : newValue.ToString();
                    return $"Remark changed to '{display}'";

                case ProductionEditProperty.VideoType:
                    return $"Videotype changed from '{production.VideoType}' to '{newValue}'";

                default:
                    return "Not Implemented";
            }
        }
    }
}