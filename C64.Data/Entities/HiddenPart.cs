using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class HiddenPart
    {
        public int HiddenPartId { get; set; }

        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        [MaxLength(65535)]
        public string Description { get; set; }
    }
}