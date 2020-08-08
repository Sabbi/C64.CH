using C64.Data.Entities;
using System.Collections.Generic;

namespace C64.FrontEnd.Models
{
    public class EditProductionPictures
    {
        public List<ProductionPictureExtended> ProductionPictures { get; set; } = new List<ProductionPictureExtended>();
    }

    public class ProductionPictureExtended : ProductionPicture
    {
        public string Base64Data { get; set; }
    }
}