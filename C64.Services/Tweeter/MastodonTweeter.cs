using Mastonet;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public class MastodonTweeter : ITweeter, IPictureTweeter
    {
        private readonly string server;
        private readonly string username;
        private readonly string password;
        private readonly string logoPath;
        private readonly ILogger<MastodonTweeter> logger;
        private HttpClient httpClient;

        public MastodonTweeter(string server, string username, string password, string logoPath, ILogger<MastodonTweeter> logger)
        {
            httpClient = new HttpClient();
            this.server = server;
            this.username = username;
            this.password = password;
            this.logoPath = logoPath;
            this.logger = logger;
        }

        public async Task SendPictureTweet(string text, byte[] pictureData)
        {
            var mastodonClient = await CreateClient();

            var mediaDefinition = new MediaDefinition(new MemoryStream(pictureData), "picture.png");
            var mediaResult = await mastodonClient.UploadMedia(mediaDefinition);

            var medias = new List<long>();

            medias.Add(mediaResult.Id);
            await SendMediaTweet(mastodonClient, text, medias);
        }

        public async Task SendTweet(string text)
        {


            var mastodonClient = await CreateClient();

            byte[] logoBytes = null;

            if (!string.IsNullOrEmpty(logoPath))
            {
                logoBytes = File.ReadAllBytes(logoPath);
            }

            var medias = new List<long>();

            try
            {
                if (logoBytes != null)
                {
                    var mediaDefinition = new MediaDefinition(new MemoryStream(logoBytes), "logo.png");
                    var mediaResult = await mastodonClient.UploadMedia(mediaDefinition);

                    medias.Add(mediaResult.Id);
                }
                await SendMediaTweet(mastodonClient, text, medias);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Send mastodon status failed");
                var errors = e.Message;
                throw new Exception(errors);
            }
        }

        private async Task SendMediaTweet(MastodonClient mastodonClient, string text, IEnumerable<long> medias)
        {
            await mastodonClient.PostStatus(text, language: "en", mediaIds: medias);
        }

        private async Task<MastodonClient> CreateClient()
        {
            var authClient = new AuthenticationClient(server);

            var appRegistration = await authClient.CreateApp("C64.CH", Scope.Read | Scope.Write | Scope.Follow);
            var auth = await authClient.ConnectWithPassword(username, password);

            return new MastodonClient(appRegistration, auth, httpClient);
        }

    }
}