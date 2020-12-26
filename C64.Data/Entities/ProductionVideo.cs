using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace C64.Data.Entities
{
    public class ProductionVideo : ISortable
    {
        public int ProductionVideoId { get; set; }
        public VideoProvider VideoProvider { get; set; }

        [MaxLength(127)]
        public string VideoId { get; set; }

        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public int Sort { get; set; } = 0;
        public bool Show { get; set; } = true;

        public IVideoProvider Provider()
        {
            return VideoProviderFactory.GetVideoProvider(VideoProvider, VideoId);
        }
    }

    public static class VideoProviderFactory
    {
        public static IVideoProvider GetVideoProvider(VideoProvider provider, string videoId)
        {
            switch (provider)
            {
                case VideoProvider.Youtube:
                    return new YouTubeVideoProvider(videoId);
            }

            return null;
        }
    }

    public interface IVideoProvider
    {
        public string Identifier { get; }

        public string VideoUrl();

        public string ThumbnailUrl();

        public string ThumbnailUrlHq();
    }

    public class YouTubeVideoProvider : IVideoProvider
    {
        private readonly string videoId;

        public YouTubeVideoProvider(string videoId)
        {
            this.videoId = videoId;
        }

        public string Identifier => "YouTube";

        public static string ParseIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("Invalid URL");

            if (!url.Contains("youtube.com", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid URL");
            try
            {
                var querystring = new Uri(url).Query;
                var queryDictionary = HttpUtility.ParseQueryString(querystring);

                var v = queryDictionary.Get("v");

                if (string.IsNullOrEmpty(v))
                    throw new ArgumentException("Invalid URL");

                return queryDictionary.Get("v");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid URL", e);
            }
        }

        public string ThumbnailUrl()
        {
            return $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        }

        public string ThumbnailUrlHq()
        {
            return $"https://img.youtube.com/vi/{videoId}/default.jpg";
        }

        public string VideoUrl()
        {
            return $"https://www.youtube.com/watch?v={videoId}";
        }
    }
}