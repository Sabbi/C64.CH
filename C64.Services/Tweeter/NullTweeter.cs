using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public class NullTweeter : ITweeter, IPictureTweeter
    {
        public Task SendPictureTweet(string text, byte[] pictureData)
        {
            return Task.FromResult(0);
        }

        public Task SendTweet(string text)
        {
            return Task.FromResult(0);
        }
    }
}