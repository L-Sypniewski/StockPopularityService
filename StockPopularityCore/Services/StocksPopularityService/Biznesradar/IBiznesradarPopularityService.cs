using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IBiznesradarPopularityService
    {
        public Task<StocksPopularity<StockPopularityItem>> FetchStocksPopularity();
    }
}