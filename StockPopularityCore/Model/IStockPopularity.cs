using System;

namespace StockPopularityCore.Model
{
    public interface IStockPopularity
    {
        public string StockName { get; }
        public int Rank { get; }
        public DateTimeOffset Date { get; }
    }
}