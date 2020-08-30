using C64.FrontEnd.Helpers;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class AddGuestbookModel
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        [NotRequiredEmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        [NotRequiredUrl]
        public string Homepage { get; set; }

        [MaxLength(65535)]
        [Required]
        public string Comment { get; set; }
    }
}