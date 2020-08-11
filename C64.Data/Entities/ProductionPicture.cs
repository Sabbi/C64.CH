using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class ProductionPicture : ISortable
    {
        public int ProductionPictureId { get; set; }
        public int ProductionId { get; set; }

        public virtual Production Production { get; set; }

        [MaxLength(511)]
        public string Filename { get; set; }

        public int Sort { get; set; } = 0;
        public bool Show { get; set; } = true;

        public int Size { get; set; } = 0;
    }

    public interface ISortable
    {
        int Sort { get; set; }
    }
}