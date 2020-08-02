using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class ProductionFile
    {
        public int ProductionFileId { get; set; }

        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        [MaxLength(1023)]
        public string Filename { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Downloads { get; set; }

        public int Sort { get; set; } = 0;
        public bool Show { get; set; } = true;

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long Size { get; set; } = 0;

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long SizeKb => Size / 1024;

        public bool IsArchive => Filename.ToLower().EndsWith(".zip");
    }
}