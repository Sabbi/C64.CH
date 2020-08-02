using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int Value { get; set; }

        [MaxLength(40)]
        public string Ip { get; set; }

        public DateTime RatedAt { get; set; }
    }
}