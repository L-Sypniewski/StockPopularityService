using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.PopularityService
{
    public interface IPopularityService<TStockPopularityItem> where TStockPopularityItem : IStockPopularityItem
    {
        public Task<StocksPopularity<TStockPopularityItem>> FetchStocksPopularity();
    }
}