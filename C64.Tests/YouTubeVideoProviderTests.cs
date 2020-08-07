using C64.Data.Entities;
using System;
using Xunit;

namespace C64.Tests
{
    public class YouTubeVideoProviderTests
    {
        [Fact]
        public void CorrectUrlReturnsCorrectId()
        {
            var testUrl = "https://www.youtube.com/watch?v=Sq9ZZ8zilDw";

            var result = YouTubeVideoProvider.ParseIdFromUrl(testUrl);

            Assert.Equal("Sq9ZZ8zilDw", result);
        }

        [Fact]
        public void CorrectUrlReturnsCorrectIdCaseInsensitivity()
        {
            var testUrl = "https://www.youtube.com/watch?V=Sq9ZZ8zilDw";

            var result = YouTubeVideoProvider.ParseIdFromUrl(testUrl);

            Assert.Equal("Sq9ZZ8zilDw", result);
        }

        [Fact]
        public void CorrectUrlReturnsCorrectIdAdditionalQuery()
        {
            var testUrl = "https://www.youtube.com/watch?v=Sq9ZZ8zilDw&test=1&foo=bar";

            var result = YouTubeVideoProvider.ParseIdFromUrl(testUrl);

            Assert.Equal("Sq9ZZ8zilDw", result);
        }

        [Fact]
        public void CorrectUrlReturnsCorrectIdAdditionalQuery2()
        {
            var testUrl = "https://www.youtube.com/watch?test2=othervalue&v=Sq9ZZ8zilDw&test=1&foo=bar";

            var result = YouTubeVideoProvider.ParseIdFromUrl(testUrl);

            Assert.Equal("Sq9ZZ8zilDw", result);
        }

        [Fact]
        public void InvalidUrlThrowsException()
        {
            var testUrl = "https://www.youtube.com/watch?test2=othoo=bar";
            Assert.Throws<ArgumentException>(() => YouTubeVideoProvider.ParseIdFromUrl(testUrl));
        }

        [Fact]
        public void InvalidUrlThrowsException2()
        {
            var testUrl = "watch?v=Sq9ZZ8zilDw&test=1&foo=bar";
            Assert.Throws<ArgumentException>(() => YouTubeVideoProvider.ParseIdFromUrl(testUrl));
        }

        [Fact]
        public void InvalidUrlThrowsException3()
        {
            var testUrl = "ttps://www.vimeo.com/watch?v=Sq9ZZ8zilDw&test=1&foo=bar";
            Assert.Throws<ArgumentException>(() => YouTubeVideoProvider.ParseIdFromUrl(testUrl));
        }
    }
}