using System.Collections.Generic;
using System.Linq;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchCurrencyPairItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<PopularityItem> FetchedPopularity => _fixture.FetchedPopularity;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchCurrencyPairItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly fetch Stocknames for currency pairs")]
        [MemberData(nameof(CodenameOnlyData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_fetch_StockNames_for_currency_pairs(
                int index, StockName expectedStockName)
        {
            var actualStockNameAtIndex = FetchedPopularity.Items.ToArray()[index].StockName;
            actualStockNameAtIndex.Should()
                                  .Be(expectedStockName,
                                      $"parsed {nameof(StockName)} at index {index} should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameOnlyData => new List<object[]>
        {
            new object[] {34, new StockName("USD/PLN", "1:1 - dolar/złoty")},
            new object[] {89, new StockName("EUR/PLN", "1:1 - euro/złoty")},
        };
    }
}