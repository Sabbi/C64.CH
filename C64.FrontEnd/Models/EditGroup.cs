using C64.FrontEnd.Helpers;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditGroup
    {
        [Required]
        public string Name { get; set; }

        public string Aka { get; set; }

        public string Description { get; set; }

        public PartialDate FoundedDate { get; set; }
        public PartialDate ClosedDate { get; set; }

        [NotRequiredUrl]
        public string Url { get; set; }

        [NotRequiredEmailAddress]
        public string Email { get; set; }

        public bool CanEditName { get; set; } = false;
    }
}