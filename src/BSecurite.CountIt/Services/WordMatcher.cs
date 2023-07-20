using System.Text.RegularExpressions;
using BSecurite.CountIt.Abstractions;

namespace BSecurite.CountIt.Services
{
    public class WordMatcher : IWordMatcher
    {
        private const string NumberRegEx = @"\d+";
        private const string WordRegEx = @"\w+|W+";

        public List<string> ExtractWords(string inputText)
        {
            var allMatches = Regex.Matches(inputText, WordRegEx);

            var allMatchedWords = allMatches
                .Where(x => !Regex.IsMatch(x.Value, NumberRegEx))
                .Select(x => x.Value)
                .ToList();

            return allMatchedWords;
        }
    }
}
