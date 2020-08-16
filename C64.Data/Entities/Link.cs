using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Link
    {
        public int LinkId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }

        public int LinkCategoryId { get; set; }
        public virtual LinkCategory LinkCategory { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public int Hits { get; set; }
    }
}