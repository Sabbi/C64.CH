using C64.Data.Models;
using C64.FrontEnd.Helpers;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditParty
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public PartialDate From { get; set; } = new PartialDate();
        public PartialDate To { get; set; } = new PartialDate();
        public string Organizers { get; set; }

        [NotRequiredUrl]
        public string Url { get; set; }

        [NotRequiredEmailAddress]
        public string Email { get; set; }

        public string Location { get; set; }
        public string CountryId { get; set; }
    }
}