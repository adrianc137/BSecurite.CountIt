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
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Provided input file path does not exist.", nameof(filePath));
            }

            try
            {
                _logger.LogDebug("Started reading contents of {FilePath}", filePath);

                var fileContents = await File.ReadAllLinesAsync(filePath);

                var fileContentsList = fileContents
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToArray();

                _logger.LogDebug("Finished reading file contents, with a total of {0} lines read",
                    fileContentsList.Length);

                return fileContentsList;
            }
            catch (IOException ioEx)
            {
                _logger.LogError(ioEx, "The program failed to run due to an unexpected I/O exception.");
                throw;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                _logger.LogError(uaEx, "The program failed to run due to an unauthorized access exception.");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "The program failed to run due to an unexpected exception.");
                throw;
            }
        }
    }
}
