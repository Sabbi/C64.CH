using C64.Data;
using C64.Data.Entities;
using C64.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class EditProductionGeneral
    {
        [Required]
        public string Name { get; set; }

        public string Aka { get; set; }

        public PartialDate ReleasedOn { get; set; }

        public Platform Platform { get; set; }

        public TopCategory TopCategory { get; set; }
        public SubCategory SubCategory { get; set; }

        public IList<Group> SelectedGroups { get; set; } = new List<Group>();
        public IList<Scener> SelectedSceners { get; set; } = new List<Scener>();

        public Party Party { get; set; }

        [Range(0, 99, ErrorMessage = "Must be between 0 (unknown) and 99")]
        public int PartyRank { get; set; }

        public string PartyCategoryId { get; set; }

        public bool ForceCreate { get; set; }
    }
}