namespace BSecurite.CountIt.Abstractions
{
    public interface IOutputGenerator
    {
        /// <summary>
        /// Prints the results in the following format
        /// Number of words: xx
        /// word1: # of occurrences
        /// word2: # of occurrences
        /// ...
        /// and so on
        /// </summary>
        /// <param name="results">A dictionary where the key is the word and the value is the nr of occurrences</param>
        void PrintResults(Dictionary<string, int> results);
    }
}
