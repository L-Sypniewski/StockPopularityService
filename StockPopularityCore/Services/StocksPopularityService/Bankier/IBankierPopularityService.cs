using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public interface IBankierPopularityService
    {
        public Task<StockPopularity<BankierStockPopularityItem>> FetchStockPopularity();
    }
}