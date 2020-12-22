using System.Collections.Generic;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.Biznesradar
{
    public interface IBiznesradarPopularityService
    {
        IAsyncEnumerable<IStockPopularity> StocksPopularity();
    }
}