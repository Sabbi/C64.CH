using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace C64.Data.Entities
{
    public class Scener
    {
        public int ScenerId { get; set; }
        public string Handle { get; set; }

        [MaxLength(255)]
        public string RealName { get; set; }

        public bool ShowRealName { get; set; } = true;
        public virtual ICollection<ScenersGroups> ScenersGroups { get; set; } = new HashSet<ScenersGroups>();
        public virtual ICollection<ScenersJobs> Jobs { get; set; } = new HashSet<ScenersJobs>();

        public virtual ICollection<ScenersSceners> AlterEgos { get; set; } = new HashSet<ScenersSceners>();

        public virtual ICollection<PartiesSceners> PartiesSceners { get; set; } = new HashSet<PartiesSceners>();

        public string HandleWithGroups()
        {
            var sb = new StringBuilder();

            sb.Append(Handle);

            if (ScenersGroups.Any())
            {
                sb.Append(" of " + string.Join(",", ScenersGroups.Select(p => p.Group?.Name)));
            }

            return sb.ToString();
        }
    }
}