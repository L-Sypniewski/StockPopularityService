using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.PopularityService
{
    public interface IBankierPopularityService
    {
        public Task<StocksPopularity<BankierStockPopularityItem>> FetchStocksPopularity();
    }
}