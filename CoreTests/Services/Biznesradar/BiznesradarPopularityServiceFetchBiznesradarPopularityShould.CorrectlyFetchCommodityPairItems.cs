using System.Collections.Generic;
using System.Linq;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchCommodityItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<BiznesradarPopularityItem> FetchedPopularity => _fixture.FetchedPopularity;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchCommodityItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly fetch Stocknames for commodites")]
        [MemberData(nameof(CodenameOnlyData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_fetch_StockNames_for_commodities(
                int index, StockName expectedStockName)
        {
            var actualStockNameAtIndex = FetchedPopularity.Items.ToArray()[index].StockName;
            actualStockNameAtIndex.Should()
                                  .Be(expectedStockName,
                                      $"parsed {nameof(StockName)} at index {index} should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameOnlyData => new List<object[]>
        {
            new object[] {65, new StockName("Ropa Brent", "Brent Crude Oil Spot")},
            new object[] {79, new StockName("Ropa naftowa", "WTI Light Crude Oil Spot")},
            new object[] {72, new StockName("Gaz ziemny", "Natural Gas Futures")},
        };
    }
}