using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IStocksPopularityService<TStockPopularityItem> where TStockPopularityItem : IStockPopularityItem
    {
        public Task<StocksPopularity<TStockPopularityItem>> FetchStocksPopularity();
    }
}