using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class ProductionFilesApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<List<ProductionFile>>(historyProduction.NewValue);

            production.ProductionFiles.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionFiles.Add(new ProductionFile { ProductionId = newValue.ProductionId, ProductionFileId = newValue.ProductionFileId, Description = newValue.Description, Downloads = newValue.Downloads, Show = newValue.Show, Sort = newValue.Sort, Filename = newValue.Filename, Size = newValue.Size });
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newStrippedValues = new List<ProductionFile>();
            var oldStrippedValues = new List<ProductionFile>();

            foreach (var newFile in (List<ProductionFile>)newValue)
                newStrippedValues.Add(new ProductionFile { ProductionId = newFile.ProductionId, Description = newFile.Description, Downloads = newFile.Downloads, ProductionFileId = newFile.ProductionFileId, Show = newFile.Show, Sort = newFile.Sort, Filename = newFile.Filename, Size = newFile.Size });

            foreach (var oldFile in production.ProductionFiles)
                oldStrippedValues.Add(new ProductionFile { ProductionId = oldFile.ProductionId, Description = oldFile.Description, Downloads = oldFile.Downloads, ProductionFileId = oldFile.ProductionFileId, Show = oldFile.Show, Sort = oldFile.Sort, Filename = oldFile.Filename, Size = oldFile.Size });

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "ProductionFiles",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionFileId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionFileId)),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M
            };
            return dbhistory;
        }
    }
}