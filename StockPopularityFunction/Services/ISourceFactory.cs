using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public interface ISourceFactory
    {
        Source<Ranking> CreateFrom(StockPopularity<IStockPopularityItem> stockPopularity);
    }
}