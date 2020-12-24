using System.Threading.Tasks;
using StockPopularityCore.Model;

namespace StockPopularityCore.Services.Bankier
{
    public interface IBankierPopularityService
    {
        public Task<StocksPopularity<BankierStockPopularityItem>> FetchStocksPopularity();
    }
}