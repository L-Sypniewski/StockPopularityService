using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BankierRanking : Ranking
    {
        public int postsFromLast30DaysCount { get; }


        public BankierRanking(string name, int rank, int postsFromLast30DaysCount) : base(name, rank)
        {
            this.postsFromLast30DaysCount = postsFromLast30DaysCount;
        }
    }
}