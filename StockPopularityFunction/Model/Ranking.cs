using System.Diagnostics.CodeAnalysis;
using StockPopularityCore.Model;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Ranking
    {
        public string name { get; }
        public string? fullName { get; }
        public int rank { get; }


        public Ranking(string name, int rank, string? fullName = null)
        {
            this.name = name;
            this.rank = rank;
            this.fullName = fullName;
        }

        public Ranking(StockName stockName, int rank) : this(stockName.Codename, rank, stockName.LongName) {
        }
    }
}