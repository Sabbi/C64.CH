using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class FeedbackModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}