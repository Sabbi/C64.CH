using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class LinkCategory
    {
        public int LinkCategoryId { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        public bool Selectable { get; set; }
    }
}