using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public interface ITweeter
    {
        public Task SendTweet(string text);
    }
}