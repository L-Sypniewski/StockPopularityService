using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IStockPopularityService<TStockPopularityItem> where TStockPopularityItem : IStockPopularityItem
    {
        public Task<StockPopularity<TStockPopularityItem>> FetchStockPopularity();
    }
}