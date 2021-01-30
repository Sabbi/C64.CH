using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public class SiteInfo
    {
        public int SiteInfoId { get; set; }

        public int Category { get; set; } = 0;

        public DateTime PublishedAt { get; set; }

        public DateTime? TweetedAt { get; set; }

        [MaxLength(65535)]
        public string Text { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Writer { get; set; }

        public bool Show { get; set; } = true;

        [MinLength(36)]
        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [NotMapped]
        public string WriterFixed
        {
            get
            {
                if (!string.IsNullOrEmpty(Writer))
                {
                    if (Category != 0)
                        return $"Contributed by {Writer}";
                    else
                        return Writer;
                }

                if (User != null)
                    return User.UserName;

                return "Anonymous";
            }
        }

        public string TextExcerpt(int maxLength = 40)
        {
            if (Text == null || Text.Length <= maxLength)
                return "";

            return Text.Substring(0, maxLength) + "...";
        }
    }
}