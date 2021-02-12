using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public interface IPictureTweeter
    {
        public Task SendPictureTweet(string text, byte[] pictureData);
    }
}