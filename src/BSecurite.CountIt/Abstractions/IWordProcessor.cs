namespace BSecurite.CountIt.Abstractions;

public interface IWordProcessor
{
    /// <summary>
    /// Given an input file, it will extract each word and calculate its number of occurrences
    /// </summary>
    /// <param name="inputFilePath"></param>
    /// <returns></returns>
    Task<Dictionary<string, int>> ProcessFileContents(string inputFilePath);
}