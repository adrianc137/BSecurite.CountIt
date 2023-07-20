using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using NUnit.Framework;

namespace BSecurite.CountIt.Tests
{
    [TestFixture]
    public class OutputGeneratorTests
    {
        private IOutputGenerator _outputGenerator;

        [SetUp]
        public void SetUp()
        {
            _outputGenerator = new OutputGenerator();
        }

        [Test]
        public void OutputGenerator_WhenCorrectInput_ShouldDisplayAllValues()
        {
            var words = new Dictionary<string, int>
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

            var expectedResults = @"Number of words: 23"
                                  + Environment.NewLine + "big 3"
                                  + Environment.NewLine + "brown 3"
                                  + Environment.NewLine + "dog 2"
                                  + Environment.NewLine + "fox 3"
                                  + Environment.NewLine + "jumped 2"
                                  + Environment.NewLine + "lazy 2"
                                  + Environment.NewLine + "number 1"
                                  + Environment.NewLine + "over 2"
                                  + Environment.NewLine + "the 5"
                                  + Environment.NewLine;

            using var sw = new StringWriter();
            Console.SetOut(sw);

            _outputGenerator.PrintResults(words);

            Assert.AreEqual(expectedResults, sw.ToString());
        }
    }
}
