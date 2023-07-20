using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class WordSorterTests
    {
        private IWordSorter _wordSorter;

        [SetUp]
        public void SetUp()
        {
            _wordSorter = new WordSorter();
        }

        [Test]
        public void WordSorter_WhenValidInputLetters_ShouldSortCorrectly()
        {
            var input = new List<string> { "c", "a", "b" };
            var expected = new List<string> { "a", "b", "c" };

            Assert.AreEqual(expected, _wordSorter.SortWords(input));
        }

        [Test]
        public void WordSorter_InputWordsSameLength_ShouldSortCorrectly()
        {
            var input = new List<string> { "caa", "cca", "bbb" };
            var expected = new List<string> { "bbb", "caa", "cca" };

            Assert.AreEqual(expected, _wordSorter.SortWords(input));
        }

        [Test]
        public void WordSorter_InputWordsDifferentLength_ShouldSortCorrectly()
        {
            var input = new List<string> { "the", "big", "brown", "fox", "jumped", "over", "lazy", "dog" };
            var expected = new List<string> { "big", "brown", "dog", "fox", "jumped", "lazy", "over", "the" };

            Assert.AreEqual(expected, _wordSorter.SortWords(input));
        }

        [Test]
        public void WordSorter_EmptyList_ShouldReturnEmptyList()
        {
            var input = new List<string>();
            var expected = new List<string>();

            Assert.AreEqual(expected, _wordSorter.SortWords(input));
        }
    }
}
