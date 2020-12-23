using System.Collections.Generic;
using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.Biznesradar
{
    public interface IBiznesradarPopularityService
    {
        public Task<StocksPopularity<StockPopularityItem>> FetchStocksPopularity();
    }
}