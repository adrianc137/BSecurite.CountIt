using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class WordProcessorTests
    {
        private IWordProcessor _wordProcessor;
        private Mock<IFileReader> _fileReaderMock;
        private Mock<IWordSorter> _wordSorterMock;
        private Mock<IWordMatcher> _wordMatcherMock;
        private Mock<ILogger<IWordMatcher>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _wordSorterMock = new Mock<IWordSorter>();
            _wordMatcherMock = new Mock<IWordMatcher>();
            _loggerMock = new Mock<ILogger<IWordMatcher>>();

            _wordProcessor = new WordProcessor(_fileReaderMock.Object, _wordSorterMock.Object, _wordMatcherMock.Object, _loggerMock.Object);
        }

        [Test]
        public void WordCounter_WhenInputNotProvided_ShouldThrowArgumentException()
        {
            var inputFilePath = string.Empty;
            Assert.That(() => _wordProcessor.ProcessFileContents(inputFilePath), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void WordCounter_WhenInputFileDoesntExist_ShouldThrowArgumentException()
        {
            var inputFilePath = "invalidFilePath.txt";
            Assert.That(() => _wordProcessor.ProcessFileContents(inputFilePath), 
                Throws.InstanceOf<ArgumentException>()
                    .With
                    .Message
                    .Contains("Provided input file path does not exist."));
        }
    }
}