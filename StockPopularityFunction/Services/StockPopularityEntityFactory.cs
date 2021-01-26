using System;
using System.Linq;
using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public interface IStockPopularityEntityFactory
    {
        StockPopularityEntity<IRanking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity);
    }

    public class StockPopularityEntityFactory : IStockPopularityEntityFactory
    {
        public StockPopularityEntity<IRanking> CreateEntities(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var itemType = stockPopularity.Items.First().GetType();
            if (itemType == typeof(BankierStockPopularityItem))
            {
                return CreateEntitiesForBankier(stockPopularity);
            }

            if (itemType == typeof(StockPopularityItem))
            {
                return CreateEntitiesForBankier(stockPopularity);
            }

            throw new ArgumentException(
                "IStockPopularityItem has not been handled by the factory yet, implement missing feature!");
        }


        private static StockPopularityEntity<IRanking> CreateEntitiesForBankier(
            StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<BankierStockPopularityItem>();

            var source = new Source<IRanking>(
                "Bankier", casted.Items
                                 .Select(x => new BankierRanking(x.StockName.Codename, x.Rank, x.PostsFromLast30DaysCount)));
            return new StockPopularityEntity<IRanking>(casted.DateTime, source);
        }


        private StockPopularityEntity<IRanking> CreateEntitiesForBiznesradar(
            StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<StockPopularityItem>();

            var source = new Source<IRanking>(
                "Bankier", casted.Items
                                 .Select(x => new Ranking(x.StockName.Codename, x.Rank)));
            return new StockPopularityEntity<IRanking>(casted.DateTime, source);
        }
    }
}