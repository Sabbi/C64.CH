using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditPasswordModel
    {
        [MinLength(6)]
        [MaxLength(255)]
        [Required]
        public string CurrentPassword { get; set; }

        [MinLength(6)]
        [MaxLength(255)]
        [Required]
        public string NewPassword { get; set; }

        [MinLength(6)]
        [MaxLength(255)]
        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}