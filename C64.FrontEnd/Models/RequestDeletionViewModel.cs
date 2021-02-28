using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class RequestDeletionViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(4095)]
        public string Comment { get; set; }
    }
}