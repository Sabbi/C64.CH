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
                Version = 1M
            };
            return dbhistory;
        }
    }
}