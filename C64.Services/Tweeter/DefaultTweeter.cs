using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace C64.Services.Tweeter
{
    public class DefaultTweeter : ITweeter, IPictureTweeter
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

        public async Task SendPictureTweet(string text, byte[] pictureData)
        {
            var media = await client.Upload.UploadTweetImageAsync(pictureData);
            await SendMediaTweet(text, new List<IMedia>() { media });
        }

        public async Task SendTweet(string text)
        {
            IMedia uploadedImage = null;
            if (!string.IsNullOrEmpty(logoPath))
            {
                var logo = File.ReadAllBytes(logoPath);
                uploadedImage = await client.Upload.UploadTweetImageAsync(logo);
            }

            var medias = new List<IMedia>();

            if (uploadedImage != null)
                medias = new List<IMedia>() { uploadedImage };

            await SendMediaTweet(text, medias);
        }

        private async Task SendMediaTweet(string text, IEnumerable<IMedia> medias)
        {
            var tweetParams = new PublishTweetParameters(text);
            tweetParams.Medias = medias.ToList();

            try
            {
                var result = await client.Tweets.PublishTweetAsync(tweetParams);
                logger.LogInformation("Sent tweet {text}, Result {@result}", text, result);
            }
            catch (TwitterException e)
            {
                logger.LogError(e, "Send tweet failed");
                var errors = string.Join(", ", e.TwitterExceptionInfos.Select(p => p.Message));
                throw new Exception(errors);
            }
        }
    }
}