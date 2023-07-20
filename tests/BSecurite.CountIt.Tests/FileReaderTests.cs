using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        private IFileReader _fileReader;
        private Mock<ILogger<IFileReader>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<IFileReader>>();
            _fileReader = new FileReader(_loggerMock.Object);
        }

        [Test]
        public async Task FileReader_WhenValidInput_ShouldReadContents()
        {
            var expectedOutput = new[] { "aaa bbb ccc", "a1a1", "1111" };

            var fileContents = await _fileReader.ReadContents("inputFiles\\validInput.txt");
            Assert.AreEqual(expectedOutput, fileContents);
        }

        [Test]
        public async Task FileReader_WhenEmptyInput_ShouldReturnEmptyArray()
        {
            var fileContents = await _fileReader.ReadContents("inputFiles\\emptyInput.txt");
            Assert.AreEqual(Array.Empty<string>(), fileContents);
        }

        [TearDown]
        public void TearDown()
        {
            _fileReader = null;
            _loggerMock = null;
        }
    }
}
