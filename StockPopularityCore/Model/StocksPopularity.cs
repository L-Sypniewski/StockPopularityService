using System;
using System.Collections.Generic;
using System.Linq;

namespace StockPopularityCore.Model
{
    public readonly struct StockPopularity<TStockPopularityItem> where TStockPopularityItem : IStockPopularityItem
    {
        public IReadOnlyCollection<TStockPopularityItem> Items { get; }
        public DateTimeOffset DateTime { get; }


        public StockPopularity(IEnumerable<TStockPopularityItem> items, DateTimeOffset dateTime)
        {
            Items = items.ToArray();
            DateTime = dateTime;
        }


        public StockPopularity<T> Casted<T>() where T : IStockPopularityItem
        {
            return new StockPopularity<T>(Items.Cast<T>(), DateTime);
        }
    }
}