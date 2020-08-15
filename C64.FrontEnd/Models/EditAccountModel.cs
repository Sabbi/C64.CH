using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditAccountModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Username { get; set; }
    }
}