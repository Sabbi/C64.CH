using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class ScenerJobsApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var scener = (Scener)entity;
            var newValues = JsonConvert.DeserializeObject<IEnumerable<Job>>(historyProduction.NewValue);

            scener.Jobs.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
            {
                scener.Jobs.Add(new ScenersJobs() { Job = newValue });
            }
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var scener = (Scener)entity;

            var newValues = (IEnumerable<Job>)newValue;

            var oldValues = scener.Jobs.Select(p => p.Job);

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = null,
                AffectedScenerId = scener.ScenerId,
                AffectedEntity = historyEntity,
                Property = "ScenerJobs",
                NewValue = newValues == null ? null : JsonConvert.SerializeObject(newValues),
                OldValue = oldValues == null ? null : JsonConvert.SerializeObject(oldValues),
                Status = status,
                Type = typeof(IEnumerable<int>).FullName,
                Version = 1M,
                Description = $"Scener jobs changed to '{string.Join(", ", newValues)}'"
            };

            return dbhistory;
        }
    }
}