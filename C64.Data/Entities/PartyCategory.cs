using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class PartyCategory
    {
        public int PartyCategoryId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public bool Selectable { get; set; } = true;
    }
}