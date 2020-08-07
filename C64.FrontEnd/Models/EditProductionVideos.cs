using C64.Data.Entities;
using System.Collections.Generic;

namespace C64.FrontEnd.Models
{
    public class EditProductionVideos
    {
        public string NewVideoUrl { get; set; }

        public List<ProductionVideo> ProductionVideos { get; set; } = new List<ProductionVideo>();
    }
}