using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.PopularityService
{
    public interface IBiznesradarPopularityService
    {
        public Task<StocksPopularity<StockPopularityItem>> FetchStocksPopularity();
    }
}