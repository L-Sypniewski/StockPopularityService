using System;
using System.Linq;
using AzureFunctions.Model;
using Core.Model;

namespace AzureFunctions.Services
{
    public class SourceFactory : ISourceFactory
    {
        public Source<Ranking> CreateFrom(Popularity<IPopularityItem> popularity)
        {
            var itemType = popularity.Items.First().GetType();

            if (itemType == typeof(BankierPopularityItem)) return CreateSourceForBankier(popularity);

            if (itemType == typeof(PopularityItem)) return CreateSourceForBiznesradar(popularity);

            throw new ArgumentException(
                "IPopularityItem has not been handled by the factory yet, implement missing feature!");
        }


        private static Source<Ranking> CreateSourceForBankier(Popularity<IPopularityItem> popularity)
        {
            var casted = popularity.Casted<BankierPopularityItem>();

            return new Source<Ranking>("Bankier", casted.Items
                                                        .Select(x => new BankierRanking(x.StockName, x.Rank,
                                                                    x.PostsFromLast30DaysCount)));
        }


        private static Source<Ranking> CreateSourceForBiznesradar(Popularity<IPopularityItem> popularity)
        {
            var casted = popularity.Casted<PopularityItem>();

            return new Source<Ranking>("Biznesradar", casted.Items
                                                        .Select(x => new Ranking(x.StockName, x.Rank)));
        }
    }
}
