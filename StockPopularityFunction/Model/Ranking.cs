using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Ranking
    {
        public string name { get; }
        public int rank { get; }


        public Ranking(string name, int rank)
        {
            this.name = name;
            this.rank = rank;
        }
    }
}