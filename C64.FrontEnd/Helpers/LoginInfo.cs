using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Helpers
{
    public class LoginInfo
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Persistent { get; set; }
    }
}