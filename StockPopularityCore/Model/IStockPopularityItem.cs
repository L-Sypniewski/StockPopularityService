using System;

namespace StockPopularityCore.Model
{
    public interface IStockPopularityItem
    {
        public string StockName { get; }
        public int Rank { get; }
    }
}