namespace BSecurite.CountIt.Abstractions;

public interface IWordMatcher
{
    List<string> ExtractWords(string inputText);
}