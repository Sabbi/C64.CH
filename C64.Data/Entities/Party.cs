using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Party
    {
        public int PartyId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid Date")]
        public DateTime From { get; set; } = DateTime.MinValue;

        [Required]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid Date")]
        public DateTime To { get; set; } = DateTime.MinValue;

        [MaxLength(65535)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }

        [EmailAddress]
        [MaxLength(63)]
        public string Email { get; set; }

        [MaxLength(1023)]
        public string Organizers { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<ProductionsParties> ProductionsParties { get; set; } = new HashSet<ProductionsParties>();

        public virtual ICollection<PartiesGroups> PartiesGroups { get; set; } = new HashSet<PartiesGroups>();
        public virtual ICollection<PartiesSceners> PartiesSceners { get; set; } = new HashSet<PartiesSceners>();

        public bool IsRunning()
        {
            return (From <= DateTime.Now && To >= DateTime.Now);
        }

        public int StartsInDays()
        {
            return (To - DateTime.Now).Days;
        }
    }
}