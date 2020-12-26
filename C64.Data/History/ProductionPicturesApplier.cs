using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C64.Data.History
{
    public class ProductionPicturesApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValues = JsonConvert.DeserializeObject<List<ProductionPicture>>(historyProduction.NewValue);

            production.ProductionPictures.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionPictures.Add(new ProductionPicture { ProductionId = newValue.ProductionId, ProductionPictureId = newValue.ProductionPictureId, Show = newValue.Show, Sort = newValue.Sort, Filename = newValue.Filename, Size = newValue.Size });
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newStrippedValues = new List<ProductionPicture>();
            var oldStrippedValues = new List<ProductionPicture>();

            foreach (var newPicture in (List<ProductionPicture>)newValue)
                newStrippedValues.Add(new ProductionPicture { ProductionId = newPicture.ProductionId, ProductionPictureId = newPicture.ProductionPictureId, Show = newPicture.Show, Sort = newPicture.Sort, Filename = newPicture.Filename, Size = newPicture.Size });

            foreach (var oldPicture in production.ProductionPictures)
                oldStrippedValues.Add(new ProductionPicture { ProductionId = oldPicture.ProductionId, ProductionPictureId = oldPicture.ProductionPictureId, Show = oldPicture.Show, Sort = oldPicture.Sort, Filename = oldPicture.Filename, Size = oldPicture.Size });

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                Property = "ProductionPictures",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionPictureId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionPictureId)),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M,
                Description = CreateDescription(oldStrippedValues, newStrippedValues)
            };

            return dbhistory;
        }

        private string CreateDescription(List<ProductionPicture> oldValues, List<ProductionPicture> newValues)
        {
            var sbAdded = new StringBuilder();
            var sbSortChanged = new StringBuilder();
            var sbHide = new StringBuilder();
            var sbShow = new StringBuilder();

            if (newValues.Count() > oldValues.Count())
            {
                sbAdded.Append("Added pictures: ");
                // Addd pictures?
                var added = false;
                foreach (var newValue in newValues)
                {
                    if (!oldValues.Select(p => p.Filename).Contains(newValue.Filename))
                    {
                        added = true;
                        sbAdded.Append(newValue.Filename + ", ");
                    }
                }

                if (added)
                {
                    sbAdded.Remove(sbAdded.Length - 2, 2);
                }
                else
                    sbAdded.Clear();
            }

            // Sort changed?
            var changedSort = false;
            for (var i = 0; i < oldValues.Count(); i++)
            {
                var correspondingNew = newValues.FirstOrDefault(p => p.Filename == oldValues[i].Filename);

                if (correspondingNew != null)
                    if (correspondingNew.Sort != oldValues[i].Sort)
                        changedSort = true;
            }

            if (changedSort)
                sbSortChanged.Append("Changed order of pictures");

            // Hide Changed?

            var foundChanged = false;

            foreach (var newValue in newValues.Where(p => p.Show == false))
            {
                var oldValue = oldValues.FirstOrDefault(p => p.Filename == newValue.Filename);

                if (oldValue != null && oldValue.Show == true)
                {
                    if (!foundChanged)
                        sbHide.Append("Hide pictures: ");
                    foundChanged = true;

                    sbHide.Append(oldValue.Filename + ", ");
                }
            }

            if (foundChanged)
                sbHide.Remove(sbHide.Length - 2, 2);

            // UnHide Changed?

            foundChanged = false;

            foreach (var newValue in newValues.Where(p => p.Show == true))
            {
                var oldValue = oldValues.FirstOrDefault(p => p.Filename == newValue.Filename);

                if (oldValue != null && oldValue.Show == false)
                {
                    if (!foundChanged)
                        sbShow.Append("Unhide pictures: ");
                    foundChanged = true;

                    sbShow.Append(oldValue.Filename + ", ");
                }
            }

            if (foundChanged)
                sbShow.Remove(sbShow.Length - 2, 2);

            var result = sbAdded.ToString();

            var foo = new List<string>();

            if (sbAdded.Length > 0)
                foo.Add(sbAdded.ToString());

            if (sbSortChanged.Length > 0)
                foo.Add(sbSortChanged.ToString());

            if (sbHide.Length > 0)
                foo.Add(sbHide.ToString());

            if (sbShow.Length > 0)
                foo.Add(sbShow.ToString());

            var strResult = string.Join(", ", foo);

            return strResult;
        }
    }
}