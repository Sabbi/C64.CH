using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class AdminQueue
    {
        public int AdminQueueId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Processed { get; set; }

        [MaxLength(255)]
        public string CreatorName { get; set; }

        [MaxLength(255)]
        public string CreatorEMail { get; set; }

        [MinLength(36)]
        [MaxLength(36)]
        public string CreatorUserId { get; set; }

        public virtual User CreatorUser { get; set; }

        [MinLength(36)]
        [MaxLength(36)]
        public string ProcessorUserId { get; set; }

        public virtual User ProcessorUser { get; set; }

        [MaxLength(4095)]
        public string CreatorComment { get; set; }

        [MaxLength(4095)]
        public string ProcessorComment { get; set; }

        public bool ShowProcessorComment { get; set; } = true;

        public AdminQueueStatus AdminQueueStatus { get; set; } = AdminQueueStatus.New;

        public AdminQueueRequestType AdminQueueRequestType { get; set; } = AdminQueueRequestType.Generic;

        [MaxLength(4095)]
        public string Data { get; set; }
    }
}