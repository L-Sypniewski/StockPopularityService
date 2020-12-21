using System;

namespace StockPopularityCore.Model
{
    public struct BankierStockPopularity : IStockPopularity
    {
        public string StockName { get; }
        public int Rank { get; }
        public DateTimeOffset Date { get; }
        public int PostsFromLast30DaysCount { get; }


        public BankierStockPopularity(string stockName, int rank, DateTimeOffset date, int postsFromLast30DaysCount)
        {
            StockName = stockName;
            Rank = rank;
            Date = date;
            PostsFromLast30DaysCount = postsFromLast30DaysCount;
        }
    }
}