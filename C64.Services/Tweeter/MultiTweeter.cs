using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public class MultiTweeter : IMultiTweeter
    {
        private readonly ILogger<MultiTweeter> logger;
        private List<ITweeter> tweeters;
        private List<IPictureTweeter> picutureTweeters;

        public MultiTweeter(IServiceProvider serviceProvider, ILogger<MultiTweeter> logger)
        {
            tweeters = serviceProvider.GetServices<ITweeter>().ToList();
            picutureTweeters = serviceProvider.GetServices<IPictureTweeter>().ToList();
            this.logger = logger;
        }

        public async Task SendPictureTweet(string text, byte[] pictureData)
        {
            foreach (var tweet in picutureTweeters)
            {
                try
                {
                    await tweet.SendPictureTweet(text, pictureData);
                }
                catch (Exception e)
                {
                    if (tweet is MastodonTweeter)
                    {
                        logger.LogError(e, "Exception sending Mastodon-Message");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public async Task SendTweet(string text)
        {
            foreach (var tweet in tweeters)
            {
                try
                {
                    await tweet.SendTweet(text);
                }
                catch (Exception e)
                {
                    if (tweet is MastodonTweeter)
                    {
                        logger.LogError(e, "Exception sending Mastodon-Message");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}