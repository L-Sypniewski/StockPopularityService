using System;
using System.Linq;
using StockPopularityCore.Model;
using StockPopularityFunction.Model;

namespace StockPopularityFunction.Services
{
    public class SourceFactory : ISourceFactory
    {
        public Source<Ranking> CreateFrom(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var itemType = stockPopularity.Items.First().GetType();

            if (itemType == typeof(BankierStockPopularityItem)) return CreateSourceForBankier(stockPopularity);

            if (itemType == typeof(StockPopularityItem)) return CreateSourceForBiznesradar(stockPopularity);

            throw new ArgumentException(
                "IStockPopularityItem has not been handled by the factory yet, implement missing feature!");
        }


        private static Source<Ranking> CreateSourceForBankier(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<BankierStockPopularityItem>();

            return new Source<Ranking>("Bankier", casted.Items
                                                        .Select(x => new BankierRanking(x.StockName, x.Rank,
                                                                    x.PostsFromLast30DaysCount)));
        }


        private static Source<Ranking> CreateSourceForBiznesradar(StockPopularity<IStockPopularityItem> stockPopularity)
        {
            var casted = stockPopularity.Casted<StockPopularityItem>();

            return new Source<Ranking>("Biznesradar", casted.Items
                                                        .Select(x => new Ranking(x.StockName, x.Rank)));
        }
    }
}
