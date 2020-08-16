using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class AddLinkModel
    {
        public string CategoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        [Url]
        public string Url { get; set; }
    }
}