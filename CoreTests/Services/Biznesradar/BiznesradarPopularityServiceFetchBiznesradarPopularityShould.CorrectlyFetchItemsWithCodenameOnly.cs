using System.Collections.Generic;
using System.Linq;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchItemsWithCodenameOnly
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<BiznesradarPopularityItem> FetchedPopularity => _fixture.FetchedPopularity;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchItemsWithCodenameOnly(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly fetch StockNames with codename only")]
        [MemberData(nameof(CodenameOnlyData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_fetch_StockNames_with_codename_only(
                int index, StockName expectedStockName)
        {
            var actualStockNameAtIndex = FetchedPopularity.Items.ToArray()[index].StockName;
            actualStockNameAtIndex.Should()
                                  .Be(expectedStockName,
                                      $"parsed {nameof(StockName)} at index {index} should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameOnlyData => new List<object[]>
        {
            new object[] {5, new StockName("JSW")},
            new object[] {10, new StockName("WIG20")},
            new object[] {20, new StockName("DAX.FUT")},
            new object[] {29, new StockName("FW20")},
            new object[] {87, new StockName("IDH")},
        };
    }
}