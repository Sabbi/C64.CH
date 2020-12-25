using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public class Production
    {
        public int ProductionId { get; set; }

        [NotMapped]
        public int Id
        {
            get => ProductionId;
            set => ProductionId = value;
        }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Aka { get; set; }

        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

        public DateType ReleaseDateType { get; set; } = DateType.None;

        [MaxLength(65535)]
        public string Remarks { get; set; }

        public DateTime Added { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [MaxLength(255)]
        public string Uploader { get; set; }

        [MaxLength(255)]
        public string Submitter { get; set; }

        public string SubmitterUserId { get; set; }
        public virtual User SubmitterUser { get; set; }

        public Platform Platform { get; set; } = Platform.C64;

        public TopCategory TopCategory { get; set; } = TopCategory.Demos;
        public SubCategory SubCategory { get; set; } = SubCategory.Demo;

        public VideoType VideoType { get; set; }

        // Statistics
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Downloads { get; set; }

        public int NumberOfRatings { get; set; }
        public int SumOfRatings { get; set; }

        public decimal AverageRating { get; set; }
        public int Views { get; set; }

        public bool IsPartyRelease { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfHiddenParts { get; set; }
        public int NumberOfVideos { get; set; }

        // Statistics End

        public virtual ICollection<ProductionsGroups> ProductionsGroups { get; set; } = new HashSet<ProductionsGroups>();
        public virtual ICollection<ProductionsSceners> ProductionsSceners { get; set; } = new HashSet<ProductionsSceners>();

        public virtual ICollection<ProductionPicture> ProductionPictures { get; set; } = new HashSet<ProductionPicture>();
        public virtual ICollection<ProductionVideo> ProductionVideos { get; set; } = new HashSet<ProductionVideo>();

        public virtual ICollection<ProductionFile> ProductionFiles { get; set; } = new HashSet<ProductionFile>();
        public virtual ICollection<HiddenPart> HiddenParts { get; set; } = new HashSet<HiddenPart>();
        public virtual ICollection<ProductionsParties> ProductionsParties { get; set; } = new HashSet<ProductionsParties>();

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<UserFavorite> UserFavorites { get; set; } = new HashSet<UserFavorite>();

        public virtual ICollection<ProductionCredit> ProductionCredits { get; set; } = new HashSet<ProductionCredit>();

        public string UploaderFixed()
        {
            if (!string.IsNullOrEmpty(Uploader))
                return Uploader;

            if (!string.IsNullOrEmpty(User?.UserName))
                return User.UserName;

            return null;
        }

        public string SubmitterFixed()
        {
            if (!string.IsNullOrEmpty(Submitter))
                return Submitter;

            if (!string.IsNullOrEmpty(SubmitterUser?.UserName))
                return SubmitterUser.UserName;

            return null;
        }

        public decimal GetRating()
        {
            if (NumberOfRatings < 5)
                return 0M;

            return NumberOfRatings == 0 ? 0M : (decimal)SumOfRatings / NumberOfRatings;
        }

        public string GetRatingText(bool includeNumberOfRatings = false)
        {
            var numberOfRatingsText = string.Empty;

            if (NumberOfRatings < 5)
                return "n/a (Too few ratings)";

            if (includeNumberOfRatings && NumberOfRatings > 0)
                numberOfRatingsText = $" ({NumberOfRatings} rating{(NumberOfRatings > 0 ? "s" : "")})";

            return GetRating() == 0 ? $"n/a" : $"{GetRatingRounded(2):#.00} out of 10{numberOfRatingsText}";
        }

        public decimal GetRatingRounded(int decimalPlaces = 1)
        {
            return Math.Round(GetRating(), decimalPlaces);
        }
    }
}