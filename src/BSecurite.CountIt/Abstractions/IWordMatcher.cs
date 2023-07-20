namespace BSecurite.CountIt.Abstractions;

public interface IWordMatcher
{
    /// <summary>
    /// Extracts all words from the given string, while ignoring numbers
    /// </summary>
    /// <param name="inputText"></param>
    /// <returns></returns>
    List<string> ExtractWords(string inputText);
}