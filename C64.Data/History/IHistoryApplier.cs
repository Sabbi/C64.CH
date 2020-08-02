using C64.Data.Entities;
using System;

namespace C64.Data.History
{
    public interface IHistoryApplier
    {
        HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status);

        void Apply(Production production, HistoryProduction historyProduction);

        public HistoryProduction CreateHistoryProduction(string property, Production production, object newValue, HistoryStatus status)
        {
            var editProperty = (ProductionEditProperty)Enum.Parse(typeof(ProductionEditProperty), property);
            return CreateHistoryProduction(editProperty, production, newValue, status);
        }
    }
}