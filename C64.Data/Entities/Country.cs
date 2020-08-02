using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Country
    {
        [MinLength(2), MaxLength(2)]
        public string CountryId { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        public bool Selectable { get; set; }
    }
}