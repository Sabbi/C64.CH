using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
                Version = 1M
            };

            return dbhistory;
        }
    }
}