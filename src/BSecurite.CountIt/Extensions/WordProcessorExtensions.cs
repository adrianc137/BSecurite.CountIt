namespace BSecurite.CountIt.Extensions
{
    public static class WordProcessorExtensions
    {
        public static Dictionary<string, int> ToDistinctDictionary(this List<string> wordsList)
        {
            return wordsList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
