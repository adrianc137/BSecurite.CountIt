using BSecurite.CountIt.Extensions;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class WordProcessorExtensionsTests
    {
        [Test]
        public void WordProcessor_WhenValidListOfWords_ShouldCountDistinctInstances()
        {
            var inputList = new List<string> {"dog", "fox", "dog", "pig" };
            var expectedOutput = new Dictionary<string, int>
            {
                { "dog", 2 },
                { "fox", 1 },
                { "pig", 1 },
            };

            Assert.AreEqual(expectedOutput, inputList.ToDistinctDictionary());
        }

        [Test]
        public void WordProcessor_WhenJustOneWord_ShouldCountCorrectly()
        {
            var inputList = new List<string> {"dog", "dog", "dog", "dog" };
            var expectedOutput = new Dictionary<string, int>
            {
                { "dog", 4 },
            };

            Assert.AreEqual(expectedOutput, inputList.ToDistinctDictionary());
        }
    }
}
