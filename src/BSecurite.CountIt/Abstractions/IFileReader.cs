namespace BSecurite.CountIt.Abstractions
{
    public interface IFileReader
    {
        Task<string[]> ReadContents(string filePath);
    }
}
