using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public class StockPopularityEntityFactory : IStockPopularityEntityFactory
    {
        private readonly ISourceFactory _sourceFactory;


        public StockPopularityEntityFactory(ISourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public StockPopularityEntity<Ranking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var source = _sourceFactory.CreateFrom(stockPopularity);
            return new StockPopularityEntity<Ranking>(stockPopularity.DateTime, source);
        }
    }
}