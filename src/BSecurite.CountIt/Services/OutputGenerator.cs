using BSecurite.CountIt.Abstractions;

namespace BSecurite.CountIt.Services
{
    public class OutputGenerator: IOutputGenerator
    {
        public void PrintResults(Dictionary<string, int> results)
        {
            Console.WriteLine("Number of words: {0}", results.Values.Sum());

            foreach (var entry in results)
            {
                Console.WriteLine("{0} {1}", entry.Key, entry.Value);
            }
        }
    }
}
