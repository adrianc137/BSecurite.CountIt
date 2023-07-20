using BSecurite.CountIt.Abstractions;
using Microsoft.Extensions.Logging;

namespace BSecurite.CountIt.Services
{
    public class FileReader : IFileReader
    {
        private readonly ILogger _logger;
        public FileReader(ILogger<IFileReader> logger)
        {
            _logger = logger ?? throw new ArgumentNullException();
        }

        public async Task<string[]> ReadContents(string filePath)
        {
            _logger.LogDebug("Started reading contents of {FilePath}", filePath);

            var fileContents = await File.ReadAllLinesAsync(filePath);

            var fileContentsList = fileContents
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();

            _logger.LogDebug("Finished reading file contents, with a total of {0} lines read", fileContentsList.Length);

            return fileContentsList;
        }
    }
}
