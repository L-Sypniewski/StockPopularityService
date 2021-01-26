using System.Collections.Generic;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IAggregateStockPopularityService
    {
        IAsyncEnumerable<StockPopularity<IStockPopularityItem>> FetchStockPopularityRankings();
    }
}