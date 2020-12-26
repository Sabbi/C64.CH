using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public class User : IdentityUser
    {
        public int OldId { get; set; }

        public bool Newsletter { get; set; } = true;
        public bool Digest { get; set; } = true;
        public DateTime Registered { get; set; }
        public DateTime LastLogin { get; set; }

        [MaxLength(255)]
        public string Realname { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string PublicEmail { get; set; }

        [MaxLength(1023)]
        public string Homepage { get; set; }

        [MaxLength(63)]
        public string Icq { get; set; }

        public DateTime Birthday { get; set; } = DateTime.MinValue;
        public DateType BirthdayType { get; set; } = DateType.None;

        [MaxLength(255)]
        public string Location { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        [MaxLength(255)]
        public string Occupation { get; set; }

        [MaxLength(1023)]
        public string Groups { get; set; }

        [MaxLength(255)]
        public string FavDemos { get; set; }

        [MaxLength(255)]
        public string FavGroups { get; set; }

        [MaxLength(255)]
        public string Watching { get; set; }

        [MaxLength(65535)]
        public string Blabla { get; set; }

        public bool PublicRatings { get; set; } = true;

        public bool PublicFavorites { get; set; } = true;

        public int? ScenerId { get; set; }
        public virtual Scener Scener { get; set; }

        [NotMapped]
        public string PublicName
        {
            get
            {
                return UserName;
            }
        }

        public virtual ICollection<UserFavorite> Favorites { get; set; } = new HashSet<UserFavorite>();
    }
}