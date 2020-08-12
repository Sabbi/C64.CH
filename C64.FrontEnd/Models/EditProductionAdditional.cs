using C64.Data;
using C64.Data.Entities;
using System.Collections.Generic;

namespace C64.FrontEnd.Models
{
    public class EditProductionAdditional
    {
        public string Remarks { get; set; }

        public List<HiddenPart> HiddenParts { get; set; } = new List<HiddenPart>();

        public VideoType VideoType { get; set; } = VideoType.Pal;
    }
}