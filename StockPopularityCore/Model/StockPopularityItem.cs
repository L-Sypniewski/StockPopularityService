using System;

namespace StockPopularityCore.Model
{
    public readonly struct StockPopularityItem : IStockPopularityItem
    {
        public string StockName { get; }
        public int Rank { get; }


        public StockPopularityItem(string stockName, int rank)
        {
            StockName = stockName;
            Rank = rank;
        }
    }
}