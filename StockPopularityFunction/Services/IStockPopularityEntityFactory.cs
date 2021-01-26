using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public interface IStockPopularityEntityFactory
    {
        StockPopularityEntity<Ranking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity);
    }
}