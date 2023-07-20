namespace BSecurite.CountIt.Abstractions;

public interface IWordProcessor
{
    Task<Dictionary<string, int>> ProcessFileContents(string inputFilePath);
}