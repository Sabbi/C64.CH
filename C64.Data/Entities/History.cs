using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public abstract class History<T>
    {
        public int AffectedId { get; set; }
        public virtual T Affected { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Property { get; set; }

        public string Type { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime? Applied { get; set; }
        public DateTime? Undid { get; set; }

        public HistoryStatus Status { get; set; }

        public HistoryRating Rating { get; set; }
    }

    public class HistoryProduction : History<Production>
    {
        public int HistoryProductionId { get; set; }
    }

    public enum HistoryStatus
    {
        ToApply,
        Applied,
        Undid,
        Waiting
    }

    public enum HistoryRating
    {
        Ok,
        Fake,
    }
}