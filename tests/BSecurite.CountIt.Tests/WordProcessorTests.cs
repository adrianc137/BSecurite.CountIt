using System.Runtime.CompilerServices;
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
                "brown", "fox", "jumped", "over", "the", "lazy", "dog", "the", "big", "brown", "fox",
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
    }
}