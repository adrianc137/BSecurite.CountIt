namespace BSecurite.CountIt.Abstractions
{
    public interface IFileReader
    {
        /// <summary>
        /// Reads the content of a given file path
        /// </summary>
        /// <param name="filePath">The path to the input file on disk</param>
        /// <returns>An array of strings, one for each line in the file</returns>
        Task<string[]> ReadContents(string filePath);
    }
}
