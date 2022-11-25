using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public interface IMultiTweeter
    {
        public Task SendPictureTweet(string text, byte[] pictureData);
        public Task SendTweet(string text);
    }

}

