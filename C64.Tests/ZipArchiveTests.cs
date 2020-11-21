using C64.Data.Archive;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace C64.Tests
{
    public class ZipArchiveTests
    {
        private IArchiveService service;

        public ZipArchiveTests()
        {
            var testbytes = TestResources.test_zip;
            var mockLogger = new Mock<ILogger<SharpZipArchiveService>>();

            service = new SharpZipArchiveService(mockLogger.Object);
            service.Load(testbytes);
        }

        [Fact]
        public void GetCorrectFileCount()
        {
            var expected = 3;
            var actual = service.ArchiveInfo.NumberOfFiles;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCorrectD64Count()
        {
            var expected = 2;
            var actual = service.ArchiveInfo.NumberOfD64Files;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTestFileAndVerifyContens()
        {
            var expected = "TestContent";

            var bytes = service.GetFile("TestFile.txt");
            var actual = new System.Text.ASCIIEncoding().GetString(bytes);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PseudoTestAddFileIdDiz()
        {
            var archive = service.AddFileId();

            Assert.True(true);
        }

        [Fact]
        public void OpenOldPkZipFile()
        {
            var testBytes = TestResources.pkzip_zip;
            var mockLogger = new Mock<ILogger<FallbackArchiveService>>();

            var zipService = new FallbackArchiveService(mockLogger.Object);
            zipService.Load(testBytes);

            var d64Images = zipService.ArchiveInfo.NumberOfD64Files;

            Assert.Equal(2, d64Images);
        }
    }
}