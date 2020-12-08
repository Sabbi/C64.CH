using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class ProductionFilesApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValues = JsonConvert.DeserializeObject<List<ProductionFile>>(historyProduction.NewValue);

            production.ProductionFiles.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionFiles.Add(new ProductionFile { ProductionId = newValue.ProductionId, ProductionFileId = newValue.ProductionFileId, Description = newValue.Description, Downloads = newValue.Downloads, Show = newValue.Show, Sort = newValue.Sort, Filename = newValue.Filename, Size = newValue.Size });
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newStrippedValues = new List<ProductionFile>();
            var oldStrippedValues = new List<ProductionFile>();

            foreach (var newFile in (List<ProductionFile>)newValue)
                newStrippedValues.Add(new ProductionFile { ProductionId = newFile.ProductionId, Description = newFile.Description, Downloads = newFile.Downloads, ProductionFileId = newFile.ProductionFileId, Show = newFile.Show, Sort = newFile.Sort, Filename = newFile.Filename, Size = newFile.Size });

            foreach (var oldFile in production.ProductionFiles)
                oldStrippedValues.Add(new ProductionFile { ProductionId = oldFile.ProductionId, Description = oldFile.Description, Downloads = oldFile.Downloads, ProductionFileId = oldFile.ProductionFileId, Show = oldFile.Show, Sort = oldFile.Sort, Filename = oldFile.Filename, Size = oldFile.Size });

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                Property = "ProductionFiles",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionFileId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionFileId)),
                Status = status,
                Type = newValue.GetType().FullName,
                Description = CreateDescription(oldStrippedValues, newStrippedValues),
                Version = 1M
            };
            return dbhistory;
        }

        private string CreateDescription(List<ProductionFile> oldValues, List<ProductionFile> newValues)
        {
            var modifications = new List<string>();

            // Added ? -> all with ID 0 ;)
            foreach (var added in newValues.Where(p => p.ProductionFileId == 0))
            {
                modifications.Add($"added new download '{added.Filename}'");
            }

            // Hidden/Show?
            foreach (var changed in newValues.Where(p => p.ProductionFileId > 0))
            {
                var old = oldValues.FirstOrDefault(p => p.ProductionFileId == changed.ProductionFileId);

                if (old != null && old.Show && !changed.Show)
                    modifications.Add($"hide file '{changed.Filename}'");
                else if (old != null && !old.Show && changed.Show)
                    modifications.Add($"show file '{changed.Filename}'");
            }

            if (modifications.Any())
            {
                var joined = string.Join(", ", modifications);
                return joined.Substring(0, 1).ToUpper() + joined[1..];
            }

            return null;
        }
    }
}