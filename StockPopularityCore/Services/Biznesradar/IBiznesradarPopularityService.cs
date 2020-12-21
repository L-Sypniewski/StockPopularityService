using System.Collections.Generic;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.Biznesradar
{
    public interface IBiznesradarPopularityService
    {
        IReadOnlyCollection<IStockPopularity> StocksPopularity();
    }
}