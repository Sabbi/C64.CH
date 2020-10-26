using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C64.Data.History
{
    public class VideoApplier : IHistoryApplier
    {
        public void Apply(object entity, HistoryRecord historyProduction)
        {
            var production = (Production)entity;
            var newValues = JsonConvert.DeserializeObject<List<ProductionVideo>>(historyProduction.NewValue);

            production.ProductionVideos.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionVideos.Add(new ProductionVideo { ProductionId = newValue.ProductionId, ProductionVideoId = newValue.ProductionVideoId, Show = newValue.Show, Sort = newValue.Sort, VideoId = newValue.VideoId, VideoProvider = newValue.VideoProvider });
        }

        public HistoryRecord CreateHistory(HistoryEditProperty property, HistoryEntity historyEntity, object entity, object newValue, HistoryStatus status)
        {
            var production = (Production)entity;
            var newStrippedValues = new List<ProductionVideo>();
            var oldStrippedValues = new List<ProductionVideo>();

            foreach (var newVideo in (List<ProductionVideo>)newValue)
                newStrippedValues.Add(new ProductionVideo { ProductionId = newVideo.ProductionId, ProductionVideoId = newVideo.ProductionVideoId, Show = newVideo.Show, Sort = newVideo.Sort, VideoProvider = newVideo.VideoProvider, VideoId = newVideo.VideoId });

            foreach (var oldVideo in production.ProductionVideos)
                oldStrippedValues.Add(new ProductionVideo { ProductionId = oldVideo.ProductionId, ProductionVideoId = oldVideo.ProductionVideoId, Show = oldVideo.Show, Sort = oldVideo.Sort, VideoProvider = oldVideo.VideoProvider, VideoId = oldVideo.VideoId });

            var dbhistory = new HistoryRecord
            {
                AffectedProductionId = production.ProductionId,
                Property = "ProductionVideos",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionVideoId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionVideoId)),
                Status = status,
                Type = newValue.GetType().FullName,
                Version = 1M,
                Description = CreateDescription(oldStrippedValues, newStrippedValues)
            };

            return dbhistory;
        }

        private string CreateDescription(List<ProductionVideo> oldValues, List<ProductionVideo> newValues)
        {
            var sbAdded = new StringBuilder();
            var sbSortChanged = new StringBuilder();
            var sbRemove = new StringBuilder();
            var sbHide = new StringBuilder();
            var sbShow = new StringBuilder();

            if (newValues.Count() > oldValues.Count())
            {
                sbAdded.Append("Added videos: ");
                // Addd pictures?
                var added = false;
                foreach (var newValue in newValues)
                {
                    if (!oldValues.Select(p => p.VideoId).Contains(newValue.VideoId))
                    {
                        added = true;
                        sbAdded.Append(newValue.VideoId + ", ");
                    }
                }

                if (added)
                {
                    sbAdded.Remove(sbAdded.Length - 2, 2);
                }
                else
                    sbAdded.Clear();
            }

            if (newValues.Count() < oldValues.Count())
            {
                sbRemove.Append("Removed videos: ");

                var added = false;
                foreach (var oldValue in oldValues)
                {
                    if (!newValues.Select(p => p.VideoId).Contains(oldValue.VideoId))
                    {
                        added = true;
                        sbRemove.Append(oldValue.VideoId + ", ");
                    }
                }

                if (added)
                {
                    sbRemove.Remove(sbRemove.Length - 2, 2);
                }
                else
                    sbRemove.Clear();
            }

            // Sort changed?
            var changedSort = false;
            for (var i = 0; i < oldValues.Count(); ++i)
            {
                try
                {
                    if (newValues[i].Sort != oldValues[i].Sort)
                        changedSort = true;
                }
                catch
                {
                    // I should not do this...
                }
            }

            if (changedSort)
                sbSortChanged.Append("Changed order of videos");

            // Hide Changed?

            var foundChanged = false;

            foreach (var newValue in newValues.Where(p => p.Show == false))
            {
                var oldValue = oldValues.FirstOrDefault(p => p.VideoId == newValue.VideoId);

                if (oldValue != null && oldValue.Show == true)
                {
                    if (!foundChanged)
                        sbHide.Append("Hide videos: ");
                    foundChanged = true;

                    sbHide.Append(oldValue.VideoId + ", ");
                }
            }

            if (foundChanged)
                sbHide.Remove(sbHide.Length - 2, 2);

            // UnHide Changed?

            foundChanged = false;

            foreach (var newValue in newValues.Where(p => p.Show == true))
            {
                var oldValue = oldValues.FirstOrDefault(p => p.VideoId == newValue.VideoId);

                if (oldValue != null && oldValue.Show == false)
                {
                    if (!foundChanged)
                        sbShow.Append("Unhide videos: ");
                    foundChanged = true;

                    sbShow.Append(oldValue.VideoId + ", ");
                }
            }

            if (foundChanged)
                sbShow.Remove(sbShow.Length - 2, 2);

            var result = sbAdded.ToString();

            var foo = new List<string>();

            if (sbAdded.Length > 0)
                foo.Add(sbAdded.ToString());

            if (sbRemove.Length > 0)
                foo.Add(sbRemove.ToString());

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