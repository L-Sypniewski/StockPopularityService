using System;

namespace StockPopularityCore.Model
{
    public interface IStockPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }
    }
}