using C64.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace C64.Data.History
{
    public class VideoApplier : IHistoryApplier
    {
        public void Apply(Production production, HistoryProduction historyProduction)
        {
            var newValues = JsonConvert.DeserializeObject<List<ProductionVideo>>(historyProduction.NewValue);

            production.ProductionVideos.Clear();

            if (newValues == null || !newValues.Any())
                return;

            foreach (var newValue in newValues)
                production.ProductionVideos.Add(new ProductionVideo { ProductionId = newValue.ProductionId, ProductionVideoId = newValue.ProductionVideoId, Show = newValue.Show, Sort = newValue.Sort, VideoId = newValue.VideoId, VideoProvider = newValue.VideoProvider });
        }

        public HistoryProduction CreateHistoryProduction(ProductionEditProperty property, Production production, object newValue, HistoryStatus status)
        {
            var newStrippedValues = new List<ProductionVideo>();
            var oldStrippedValues = new List<ProductionVideo>();

            foreach (var newVideo in (List<ProductionVideo>)newValue)
                newStrippedValues.Add(new ProductionVideo { ProductionId = newVideo.ProductionId, ProductionVideoId = newVideo.ProductionVideoId, Show = newVideo.Show, Sort = newVideo.Sort, VideoProvider = newVideo.VideoProvider, VideoId = newVideo.VideoId });

            foreach (var oldVideo in production.ProductionVideos)
                oldStrippedValues.Add(new ProductionVideo { ProductionId = oldVideo.ProductionId, ProductionVideoId = oldVideo.ProductionVideoId, Show = oldVideo.Show, Sort = oldVideo.Sort, VideoProvider = oldVideo.VideoProvider, VideoId = oldVideo.VideoId });

            var dbhistory = new HistoryProduction
            {
                AffectedId = production.ProductionId,
                Property = "ProductionVideos",
                NewValue = newStrippedValues == null ? null : JsonConvert.SerializeObject(newStrippedValues.OrderBy(p => p.ProductionVideoId)),
                OldValue = oldStrippedValues == null ? null : JsonConvert.SerializeObject(oldStrippedValues.OrderBy(p => p.ProductionVideoId)),
                Status = status,
                Type = newValue.GetType().FullName
            };

            return dbhistory;
        }
    }
}