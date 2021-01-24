using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace C64.Services.Tweeter
{
    public class DefaultTweeter : ITweeter
    {
        private readonly ILogger<DefaultTweeter> logger;
        private TwitterClient client;

        public DefaultTweeter(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, ILogger<DefaultTweeter> logger)
        {
            var credentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            client = new TwitterClient(credentials);
            this.logger = logger;
        }

        public async Task SendTweet(string text)
        {
            var result = await client.Tweets.PublishTweetAsync(text);
            logger.LogInformation("Sent tweet {text}, Result {@result}", text, result);
        }
    }
}