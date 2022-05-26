using C64.Data.Entities;
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

        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid date")]
        public DateTime From { get; set; } = DateTime.MinValue;

        [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Invalid date")]
        [LaterOrEqualDate("From", "To-Date must be later than From-Date")]
        public DateTime To { get; set; } = DateTime.MinValue;

        public string Organizers { get; set; }

        [NotRequiredUrl]
        public string Url { get; set; }

        [NotRequiredEmailAddress]
        public string Email { get; set; }

        public string Location { get; set; }
        public string CountryId { get; set; }

        public bool ForceCreate { get; set; }

        public void LoadParty(Party party)
        {
            Name = party.Name;
            Description = party.Description;
            From = party.From;
            To = party.To;
            Organizers = party.Organizers;
            Url = party.Url;
            Email = party.Email;
            Location = party.Location;
            CountryId = party.CountryId;
        }
    }
}