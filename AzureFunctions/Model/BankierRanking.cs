using System.Diagnostics.CodeAnalysis;
using Core.Model;

namespace AzureFunctions.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BankierRanking : Ranking
    {
        public int postsFromLast30DaysCount { get; }


        public BankierRanking(string name, int rank, int postsFromLast30DaysCount) : base(name, rank)
        {
            this.postsFromLast30DaysCount = postsFromLast30DaysCount;
        }


        public BankierRanking(StockName stockName, int rank, int postsFromLast30DaysCount) : this(
            stockName.Codename, rank, postsFromLast30DaysCount)
        {
            this.postsFromLast30DaysCount = postsFromLast30DaysCount;
        }
    }
}