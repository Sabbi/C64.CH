using C64.Data.Entities;

namespace C64.Data.History
{
    public interface IHistoryApplier
    {
        HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status);

        void Apply(object entity, HistoryRecord historyProduction);
    }
}