using System.Threading.Tasks;

namespace C64.Services.Tweeter
{
    public class NullTweeter : ITweeter
    {
        public Task SendTweet(string text)
        {
            return Task.FromResult(0);
        }
    }
}