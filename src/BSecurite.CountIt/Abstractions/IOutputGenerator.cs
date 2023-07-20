namespace BSecurite.CountIt.Abstractions
{
    public interface IOutputGenerator
    {
        void PrintResults(Dictionary<string, int> results);
    }
}
