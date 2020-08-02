using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public class ScenersGroups
    {
        public int ScenersGroupsId { get; set; }
        public int ScenerId { get; set; }
        public virtual Scener Scener { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public DateTime ValidFrom { get; set; } = DateTime.MinValue;
        public DateType ValidFromType { get; set; }
        public DateTime ValidTo { get; set; } = DateTime.MaxValue;
        public DateType ValidToType { get; set; }

        [NotMapped]
        public bool Currently
        {
            get
            {
                return ValidFrom <= DateTime.Now && ValidTo >= DateTime.Now;
            }
        }

        public virtual ICollection<ScenerGroupJob> ScenerGroupJobs { get; set; } = new HashSet<ScenerGroupJob>();
    }
}