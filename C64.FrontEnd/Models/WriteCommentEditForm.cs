using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class WriteCommentEditForm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(2096)]
        public string Comment { get; set; }
    }
}