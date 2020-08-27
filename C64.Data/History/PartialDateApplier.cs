using C64.Data.Entities;
using C64.Data.Extensions;
using Newtonsoft.Json;
using System;

namespace C64.Data.History
{
    public class PartialDateApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<PartialDateApplierData>(historyProduction.NewValue);

            var property = typeof(Production).GetProperty(historyProduction.Property);
            property.SetValue(production, newValues.Date);

            var property2 = typeof(Production).GetProperty(historyProduction.Property + "Type");
            property2.SetValue(production, newValues.Type);
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newValues = (PartialDateApplierData)newValue;

            var oldValues = new PartialDateApplierData();
            var propertyName = property.ToString();

            var propInfo = typeof(Production).GetProperty(propertyName);
            var oldValue = propInfo.GetValue(production);

            oldValues.Date = (DateTime)oldValue;

            var propInfo2 = typeof(Production).GetProperty(propertyName + "Type");
            var oldValue2 = propInfo2.GetValue(production);

            oldValues.Type = (DateType)oldValue2;

            //Release date changed from 'January 2020' to 'February 2nd, 2020'"
            string description;
            if (newValues.Type == DateType.None)
                description = "Release date removed";
            else if (oldValues.Type == DateType.None)
                description = $"Release date set to '{newValues.Date.ParseDate(newValues.Type)}'";
            else
                description = $"Release date changed from '{oldValues.Date.ParseDate(oldValues.Type)}' to '{newValues.Date.ParseDate(newValues.Type)}'";

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
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
    }

    public class PartialDateApplierData
    {
        public DateTime Date { get; set; }
        public DateType Type { get; set; }
    }
}