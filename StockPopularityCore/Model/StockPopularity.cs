using System;

namespace StockPopularityCore.Model
{
    public struct StockPopularity: IStockPopularity
    {
        public string StockName { get; }
        public int Rank { get; }
        public DateTimeOffset Date { get; }


        public StockPopularity(string stockName, int rank, DateTimeOffset date)
        {
            StockName = stockName;
            Rank = rank;
            Date = date;
        }
    }
}