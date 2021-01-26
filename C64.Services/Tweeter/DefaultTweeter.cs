using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace C64.Services.Tweeter
{
    public class DefaultTweeter : ITweeter
    {
        private readonly string logoPath;
        private readonly ILogger<DefaultTweeter> logger;
        private TwitterClient client;

        public DefaultTweeter(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, string logoPath, ILogger<DefaultTweeter> logger)
        {
            var credentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            client = new TwitterClient(credentials);
            this.logoPath = logoPath;
            this.logger = logger;
        }

        public async Task SendTweet(string text)
        {
            IMedia uploadedImage = null;
            if (!string.IsNullOrEmpty(logoPath))
            {
                var logo = File.ReadAllBytes(logoPath);
                uploadedImage = await client.Upload.UploadTweetImageAsync(logo);
            }

            var tweetParams = new PublishTweetParameters(text);

            if (uploadedImage != null)
                tweetParams.Medias = new List<IMedia>() { uploadedImage };

            var result = await client.Tweets.PublishTweetAsync(tweetParams);

            logger.LogInformation("Sent tweet {text}, Result {@result}", text, result);
        }
    }
}