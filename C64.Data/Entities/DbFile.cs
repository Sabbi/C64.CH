using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class DbFile
    {
        public int DbFileId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        [MaxLength(127)]
        [Required]
        public string Container { get; set; }

        [MaxLength(255)]
        [Required]
        public string FileName { get; set; }

        public long Size { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}