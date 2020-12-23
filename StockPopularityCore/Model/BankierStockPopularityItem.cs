using System;

namespace StockPopularityCore.Model
{
    public struct BankierStockPopularityItem : IStockPopularityItem
    {
        public string StockName { get; }
        public int Rank { get; }
        public int PostsFromLast30DaysCount { get; }


        public BankierStockPopularityItem(string stockName, int rank, int postsFromLast30DaysCount)
        {
            StockName = stockName;
            Rank = rank;
            PostsFromLast30DaysCount = postsFromLast30DaysCount;
        }
    }
}