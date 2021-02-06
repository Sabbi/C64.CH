using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace C64.Data.Entities
{
    public class Scener : MainModelBase, ILinkJoinable
    {
        public int ScenerId { get; set; }

        [NotMapped]
        public int Id
        {
            get => ScenerId;
            set => ScenerId = value;
        }

        [MaxLength(255)]
        public string Handle { get; set; }

        [MaxLength(255)]
        public string Aka { get; set; }

        [MaxLength(255)]
        public string RealName { get; set; }

        public DateTime Birthday { get; set; }
        public DateType BirthdayType { get; set; }

        public string Location { get; set; }

        [MinLength(2), MaxLength(2)]
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public bool ShowRealName { get; set; } = true;

        public ICollection<ProductionsSceners> ProductionsSceners { get; set; } = new HashSet<ProductionsSceners>();

        public virtual ICollection<ScenersGroups> ScenersGroups { get; set; } = new HashSet<ScenersGroups>();
        public virtual ICollection<ScenersJobs> Jobs { get; set; } = new HashSet<ScenersJobs>();

        public virtual ICollection<ScenersSceners> AlterEgos { get; set; } = new HashSet<ScenersSceners>();

        public virtual ICollection<PartiesSceners> PartiesSceners { get; set; } = new HashSet<PartiesSceners>();

        public string HandleWithGroups()
        {
            var sb = new StringBuilder();

            sb.Append(Handle);

            if (ScenersGroups.Any(p => p.Currently))
            {
                sb.Append(" of " + string.Join(",", ScenersGroups.Where(p => p.Currently).Select(p => p.Group?.Name)));
            }

            return sb.ToString();
        }

        public KeyValuePair<int, string> KeyValue()
        {
            return new KeyValuePair<int, string>(ScenerId, Handle);
        }
    }
}