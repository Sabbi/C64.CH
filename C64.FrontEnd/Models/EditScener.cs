using C64.Data;
using C64.Data.Entities;
using C64.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace C64.FrontEnd.Models
{
    public class EditScener
    {
        [Required]
        public string Handle { get; set; }

        public string RealName { get; set; }

        public PartialDate Birthday { get; set; } = new PartialDate();

        public string Location { get; set; }

        public string CountryId { get; set; }

        public bool ShowRealName { get; set; }

        public IList<Job> SelectedJobs = new List<Job>();

        public void LoadScener(Scener scener)
        {
            Handle = scener.Handle;
            RealName = scener.RealName;
            ShowRealName = scener.ShowRealName;
            CountryId = scener.CountryId;
            Location = scener.Location;
            Birthday = new PartialDate(scener.Birthday, scener.BirthdayType);
            SelectedJobs = scener.Jobs.Select(p => p.Job).ToList();
        }
    }
}