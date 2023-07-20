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
        public async Task WordCounter_WhenValidInput_CountsCorrectly()
        {
            var fileContents =
                "The big brown fox number 4 jumped over the lazy dog. THE BIG BROWN FOX JUMPED OVER THE LAZY DOG. The Big Brown Fox 123";
            _fileReaderMock.Setup(x => x.ReadContents(It.IsAny<string>()))
                .ReturnsAsync(() => new [] { fileContents });

            var wordsList = new List<string>
            {
                "the", "big", "brown", "fox", "number", "jumped", "over", "the", "lazy", "dog", "the", "big",
                "brown", "fox", "jumped", "over", "the", "lazy", "dog", "the", "big", "brown", "fox"
            };
            _wordMatcherMock.Setup(x => x.ExtractWords(fileContents.ToLower()))
                .Returns(() => wordsList);

            var sortedWordsList = new List<string>
            {
                "big", "big", "big",
                "brown", "brown", "brown",
                "dog", "dog",
                "fox", "fox", "fox",
                "jumped", "jumped",
                "lazy", "lazy",
                "number",
                "over", "over",
                "the", "the", "the", "the", "the"
            };
            _wordSorterMock.Setup(x => x.SortWords(wordsList))
                .Returns(sortedWordsList);

            var expectedResult = new Dictionary<string, int>()
            {
                { "big", 3 },
                { "brown", 3 },
                { "dog", 2 },
                { "fox", 3 },
                { "jumped", 2 },
                { "lazy", 2 },
                { "number", 1 },
                { "over", 2 },
                { "the", 5 },
            };

            Assert.AreEqual(expectedResult, await _wordProcessor.ProcessFileContents("filePath"));
        }

        [Test]
        public async Task WordCounter_WhenInputEmpty_CountsCorrectly()
        {
            var fileContents = "     ";
            _fileReaderMock.Setup(x => x.ReadContents(It.IsAny<string>()))
                .ReturnsAsync(() => new [] { fileContents });

            var wordsList = new List<string>();
            _wordMatcherMock.Setup(x => x.ExtractWords(fileContents.ToLower()))
                .Returns(() => wordsList);
            
            _wordSorterMock.Setup(x => x.SortWords(wordsList))
                .Returns(wordsList);

            var expectedResult = new Dictionary<string, int>();

            Assert.AreEqual(expectedResult, await _wordProcessor.ProcessFileContents("filePath"));
        }

        [Test]
        public async Task WordCounter_WhenInputNumbers_CountsCorrectly()
        {
            var fileContents = "123123 1241 529291 888 333 1 1 1 1. 23 44 56 56 7.6.6 6. 6.6";
            _fileReaderMock.Setup(x => x.ReadContents(It.IsAny<string>()))
                .ReturnsAsync(() => new [] { fileContents });

            _fileReaderMock.Setup(x => x.ReadContents(It.IsAny<string>()))
                .ReturnsAsync(() => new [] { fileContents });

            var wordsList = new List<string>();
            _wordMatcherMock.Setup(x => x.ExtractWords(fileContents.ToLower()))
                .Returns(() => wordsList);
            
            _wordSorterMock.Setup(x => x.SortWords(wordsList))
                .Returns(wordsList);

            var expectedResult = new Dictionary<string, int>();

            Assert.AreEqual(expectedResult, await _wordProcessor.ProcessFileContents("filePath"));
        }

        [Test]
        public async Task WordCounter_WhenInputRandomLetters_CountsCorrectly()
        {
            var fileContents = "h a o l e h a o t";
            _fileReaderMock.Setup(x => x.ReadContents(It.IsAny<string>()))
                .ReturnsAsync(() => new [] { fileContents });

            var wordsList = new List<string>
            {
                "h", "a", "o", "l", "e", "h", "a", "o", "t"
            };
            _wordMatcherMock.Setup(x => x.ExtractWords(fileContents.ToLower()))
                .Returns(() => wordsList);

            var sortedWordsList = new List<string>
            {
                "a", "a", "e", "h", "h", "l", "o", "o", "t"
            };
            _wordSorterMock.Setup(x => x.SortWords(wordsList))
                .Returns(sortedWordsList);

            var expectedResult = new Dictionary<string, int>
            {
                { "a", 2 },
                { "e", 1 },
                { "h", 2 },
                { "l", 1 },
                { "o", 2 },
                { "t", 1 }
            };

            Assert.AreEqual(expectedResult, await _wordProcessor.ProcessFileContents("filePath"));
        }
    }
}