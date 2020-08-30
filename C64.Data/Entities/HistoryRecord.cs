using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class HistoryRecord
    {
        public int HistoryRecordId { get; set; }

        [Required]
        [MaxLength(36)]
        [MinLength(36)]
        public string TransactionId { get; set; }

        public HistoryEntity AffectedEntity { get; set; }

        public int? AffectedProductionId { get; set; }
        public virtual Production AffectedProduction { get; set; }

        public int? AffectedGroupId { get; set; }
        public virtual Group AffectedGroup { get; set; }

        public int? AffectedScenerId { get; set; }
        public virtual Scener AffectedScener { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Property { get; set; }

        public string Type { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [MaxLength(46)]
        public string IpAdress { get; set; }

        public DateTime? Applied { get; set; }
        public DateTime? Undid { get; set; }

        public decimal Version { get; set; }
        public HistoryStatus Status { get; set; }

        public HistoryRating Rating { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }
    }

    public enum HistoryEntity
    {
        Production,
        Group,
        Scener
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