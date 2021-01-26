using System;
using System.Linq;
using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public interface IStockPopularityEntityFactory
    {
        StockPopularityEntity<Ranking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity);
    }

    public class StockPopularityEntityFactory : IStockPopularityEntityFactory
    {
        public StockPopularityEntity<Ranking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var itemType = stockPopularity.Items.First().GetType();
            if (itemType == typeof(BankierStockPopularityItem))
            {
                return CreateEntitiesForBankier(stockPopularity);
            }

            if (itemType == typeof(StockPopularityItem))
            {
                return CreateEntitiesForBiznesradar(stockPopularity);
            }

            throw new ArgumentException(
                "IStockPopularityItem has not been handled by the factory yet, implement missing feature!");
        }


        private static StockPopularityEntity<Ranking> CreateEntitiesForBankier(
            StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<BankierStockPopularityItem>();

            var source = new Source<Ranking>(
                "Bankier", casted.Items
                                 .Select(x => new BankierRanking(x.StockName.Codename, x.Rank, x.PostsFromLast30DaysCount)));
            return new StockPopularityEntity<Ranking>(casted.DateTime, source);
        }


        private StockPopularityEntity<Ranking> CreateEntitiesForBiznesradar(
            StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<StockPopularityItem>();

            var source = new Source<Ranking>(
                "Bankier", casted.Items
                                 .Select(x => new Ranking(x.StockName.Codename, x.Rank)));
            return new StockPopularityEntity<Ranking>(casted.DateTime, source);
        }
    }
}