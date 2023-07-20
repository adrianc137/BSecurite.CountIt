using BSecurite.CountIt.Abstractions;

namespace BSecurite.CountIt.Services
{
    public class WordSorter: IWordSorter
    {
        public List<string> SortWords(List<string> input)
        {
            if (input.Count == 0)
            {
                return new List<string>();
            }

            for (var i = 0; i < input.Count - 1; i++)
            {
                var minPosition = i;
                for (var k = i + 1; k < input.Count; k++)
                {
                    if (string.Compare(input[k], input[minPosition], StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        minPosition = k;
                    }
                }

                (input[i], input[minPosition]) = (input[minPosition], input[i]);
            }

            return input;
        }
    }
}
