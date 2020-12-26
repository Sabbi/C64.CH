using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        [MinLength(36)]
        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [MaxLength(65535)]
        public string Text { get; set; }

        [MaxLength(40)]
        public string Ip { get; set; }

        public DateTime CommentedAt { get; set; }
    }
}