using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class WordMatcherTests
    {
        private IWordMatcher _wordMatcher;

        [SetUp]
        public void SetUp()
        {
            _wordMatcher = new WordMatcher();
        }

        [Test]
        public void WordMatcher_WhenNumbersAsWell_ShouldMatchOnlyWords()
        {
            var inputText = "The big 4 fox number 88 JUMPED over the LAzy dog.";
            var expectedOutput = new List<string>
                { "The", "big", "fox", "number", "JUMPED", "over", "the", "LAzy", "dog" };

            Assert.AreEqual(expectedOutput, _wordMatcher.ExtractWords(inputText));
        }

        [Test]
        public void WordMatcher_WhenOnlyNumbers_ShouldReturnNothing()
        {
            var inputText = "33 44123423. 233 11 24 56666 7 8.8";
            var expectedOutput = new List<string>();

            Assert.AreEqual(expectedOutput, _wordMatcher.ExtractWords(inputText));
        }
    }
}
