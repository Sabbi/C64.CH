using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C64.Data.Entities
{
    public interface ILinkJoinable
    {
        KeyValuePair<int, string> KeyValue();
    }

    public class Group : ILinkJoinable
    {
        [NotMapped]
        public int Id
        {
            get => GroupId;
            set => GroupId = value;
        }

        public int GroupId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Aka { get; set; }

        public string LogoFile { get; set; }

        [MaxLength(65535)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Url { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime FoundedDate { get; set; }
        public DateType FoundedDateType { get; set; }

        public DateTime ClosedDate { get; set; }
        public DateType ClosedDateType { get; set; }

        public DateTime Added { get; set; }

        public string AddedById { get; set; }
        public virtual User AddedBy { get; set; }
        public DateTime Modified { get; set; }

        // GroupStats
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Downloads { get; set; }

        public int NumberOfRatings { get; set; }
        public int SumOfRatings { get; set; }

        public decimal AverageRating { get; set; }
        public int Views { get; set; }

        public int NumberOfReleases { get; set; }

        [MinLength(2), MaxLength(2)]
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public ICollection<ProductionsGroups> ProductionsGroups { get; set; } = new HashSet<ProductionsGroups>();

        public ICollection<ScenersGroups> ScenersGroups { get; set; } = new HashSet<ScenersGroups>();

        public ICollection<PartiesGroups> PartiesGroups { get; set; } = new HashSet<PartiesGroups>();
        public ICollection<PartiesSceners> PartiesSceners { get; set; } = new HashSet<PartiesSceners>();

        public KeyValuePair<int, string> KeyValue()
        {
            return new KeyValuePair<int, string>(GroupId, Name);
        }
    }
}