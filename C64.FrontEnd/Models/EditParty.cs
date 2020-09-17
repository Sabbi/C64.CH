using C64.FrontEnd.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditParty
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid date range")]
        public DateTime From { get; set; } = DateTime.MinValue;

        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid date range")]
        public DateTime To { get; set; } = DateTime.MinValue;

        public string Organizers { get; set; }

        [NotRequiredUrl]
        public string Url { get; set; }

        [NotRequiredEmailAddress]
        public string Email { get; set; }

        public string Location { get; set; }
        public string CountryId { get; set; }
    }
}