using System;
using System.Collections.Generic;

namespace C64.Data.Entities
{
    public class AddGroupMember
    {
        public Scener Scener { get; set; }
        public IList<Job> SelectedJobs { get; set; } = new List<Job>();
        public DateTime JoinedDate { get; set; }
        public DateType JoinedDateType { get; set; }
        public DateTime LeftDate { get; set; }
        public DateType LeftDateType { get; set; }
    }
}