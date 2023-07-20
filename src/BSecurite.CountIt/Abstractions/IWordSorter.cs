namespace BSecurite.CountIt.Abstractions
{
    public interface IWordSorter
    {
        /// <summary>
        /// Sorts a list of words by using lexicographical comparison.
        /// O(n^2) complexity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<string> SortWords(List<string> input);
    }
}
