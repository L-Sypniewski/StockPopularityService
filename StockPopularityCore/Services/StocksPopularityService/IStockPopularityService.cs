using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IStockPopularityService
    {
        public Task<StockPopularity<IStockPopularityItem>> FetchStockPopularity();
    }
}