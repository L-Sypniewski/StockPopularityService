using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Ranking : IRanking
    {
        public string name { get; }
        public int rank { get; }


        public Ranking(string name, int rank)
        {
            this.name = name;
            this.rank = rank;
        }
    }

    public interface IRanking
    {
        string name { get; }
        int rank { get; }
    }
}