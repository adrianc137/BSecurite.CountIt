using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Extensions;
using Microsoft.Extensions.Logging;

namespace BSecurite.CountIt.Services
{
    public class WordProcessor : IWordProcessor
    {
        private readonly IFileReader _fileReader;
        private readonly IWordSorter _wordSorter;
        private readonly IWordMatcher _wordMatcher;
        private readonly ILogger<IWordMatcher> _logger;

        public WordProcessor(IFileReader fileReader, IWordSorter wordSorter, IWordMatcher wordMatcher,ILogger<IWordMatcher> logger)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _wordSorter = wordSorter ?? throw new ArgumentNullException(nameof(wordSorter));
            _wordMatcher = wordMatcher ?? throw new ArgumentNullException(nameof(wordMatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Dictionary<string, int>> ProcessFileContents(string inputFilePath)
        {
            if (string.IsNullOrWhiteSpace(inputFilePath))
                throw new ArgumentNullException(nameof(inputFilePath));

            var allWords = await BuildWordsList(inputFilePath);

            var result = SortAndCalculateDistinctWordOccurrences(allWords);

            return result;
        }

        private async Task<List<string>> BuildWordsList(string inputFilePath)
        {
            try
            {
                // read input file contents
                var inputContents = await _fileReader.ReadContents(inputFilePath);

                var inputLines = inputContents.Select(x => x.ToLower()).ToList();

                // extract words from the input text
                var wordsList = new List<string>();
                foreach (var inputLine in inputLines)
                {
                    wordsList.AddRange(_wordMatcher.ExtractWords(inputLine));
                }

                return wordsList;
            }
            catch (Exception ex)
            {
                _logger.LogError("The program failed to run due to an unexpected error.", ex);
                throw;
            }
        }

        private Dictionary<string, int> SortAndCalculateDistinctWordOccurrences(List<string> wordsList)
        {
            var sortedWordsList = _wordSorter.SortWords(wordsList);
            return sortedWordsList.ToDistinctDictionary();
        }
    }
}
