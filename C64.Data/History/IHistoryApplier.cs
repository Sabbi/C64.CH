using C64.Data.Entities;

namespace C64.Data.History
{
    public interface IHistoryApplier
    {
        HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status);

        HistoryProduction CreateHistoryProduction(string property, Production production, object newValue, HistoryStatus status);

        void Apply(Production production, HistoryProduction historyProduction);
    }
}