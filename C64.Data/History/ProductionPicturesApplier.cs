using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class ProductionPicturesApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<List<ProductionPicture>>(historyProduction.NewValue);

            production.ProductionPictures.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionPictures.Add(new ProductionPicture { ProductionId = newValue.ProductionId, ProductionPictureId = newValue.ProductionPictureId, Show = newValue.Show, Sort = newValue.Sort, Filename = newValue.Filename, Size = newValue.Size });
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newStrippedValues = new List<ProductionPicture>();
            var oldStrippedValues = new List<ProductionPicture>();

            foreach (var newPicture in (List<ProductionPicture>)newValue)
                newStrippedValues.Add(new ProductionPicture { ProductionId = newPicture.ProductionId, ProductionPictureId = newPicture.ProductionPictureId, Show = newPicture.Show, Sort = newPicture.Sort, Filename = newPicture.Filename, Size = newPicture.Size });

            foreach (var oldPicture in production.ProductionPictures)
                oldStrippedValues.Add(new ProductionPicture { ProductionId = oldPicture.ProductionId, ProductionPictureId = oldPicture.ProductionPictureId, Show = oldPicture.Show, Sort = oldPicture.Sort, Filename = oldPicture.Filename, Size = oldPicture.Size });

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "ProductionPictures",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionPictureId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionPictureId)),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M
            };

            return dbhistory;
        }
    }
}