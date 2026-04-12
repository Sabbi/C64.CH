using idunno.Bluesky;
using idunno.Bluesky.Embed;
using Mastonet;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace C64.Services.Tweeter
{
    public class BlueSkyTweeter : ITweeter, IPictureTweeter
    {
        private readonly string identifier;
        private readonly string password;
        private readonly string logoPath;
        private readonly ILogger<BlueSkyTweeter> logger;

        public BlueSkyTweeter(string identifier, string password, string logoPath, ILogger<BlueSkyTweeter> logger)
        {
            this.identifier = identifier;
            this.password = password;
            this.logoPath = logoPath;
            this.logger = logger;
        }
        public async Task SendTweet(string text)
        {
            byte[] logoBytes = null;

            if (!string.IsNullOrEmpty(logoPath))
            {
                logoBytes = File.ReadAllBytes(logoPath);
            }

            await SendPictureTweet(text, logoBytes);
        }

        public async Task SendPictureTweet(string text, byte[] pictureData)
        {
            var imageMetaData = GetImageMetaData(pictureData);

            using BlueskyAgent agent = new();

            var result = await agent.Login(identifier, password);

            if (result.Succeeded)
            {
                var uploadImage = await agent.UploadImage(pictureData, imageMetaData.MimeType, "c64.ch", new AspectRatio(imageMetaData.Width, imageMetaData.Height));
                if (uploadImage.Succeeded)
                {
                    var postResult = await agent.Post(text, uploadImage.Result);
                    if (!postResult.Succeeded)
                    {
                        logger.LogError(postResult.AtErrorDetail.Error, "BlueSky: Error send post");
                    }
                }
                else
                {
                    logger.LogError(uploadImage.AtErrorDetail.Error, "BlueSky: Error upload Image");     
                }
            }
            else
            {
                logger.LogError(result.AtErrorDetail.Error, "BlueSky: Error log in.");
            }
        }

        private (int Width,int Height, string MimeType) GetImageMetaData(byte[] imageData )
        {
            using var image = Image.Load(imageData);

            int width = image.Width;
            int height = image.Height;

            IImageFormat format = Image.DetectFormat(imageData);
            string mimeType = format.DefaultMimeType;

            return (width, height, mimeType);

        }
    }
}
