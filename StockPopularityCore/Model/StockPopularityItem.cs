using System;

namespace StockPopularityCore.Model
{
    public readonly struct StockPopularityItem : IStockPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }


        public StockPopularityItem(StockName stockName, int rank)
        {
            StockName = stockName;
            Rank = rank;
        }


        public override string ToString() => $"StockName: {StockName}, Rank: {Rank}";
    }
}