using System.Collections.Generic;
using System.Linq;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchItemsWithCodenameAndLongName
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<PopularityItem> FetchedPopularity => _fixture.FetchedPopularity;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldCorrectlyFetchItemsWithCodenameAndLongName(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly fetch StockNames with codename and long name")]
        [MemberData(nameof(CodenameAndLongNameData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_fetch_StockNames_with_codename_and_long_name(
                int index, StockName expectedStockName)
        {
            var actualStockNameAtIndex = FetchedPopularity.Items.ToArray()[index].StockName;
            actualStockNameAtIndex.Should()
                                  .Be(expectedStockName,
                                      $"parsed {nameof(StockName)} at index {index} should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameAndLongNameData => new List<object[]>
        {
            new object[] {0, new StockName("CDR", "CDPROJEKT")},
            new object[] {53, new StockName("MVP", "MARVIPOL")},
            new object[] {59, new StockName("UNT", "UNIMOT")},
            new object[] {77, new StockName("PCX", "PCCEXOL")},
            new object[] {100, new StockName("AWM", "AIRWAY")},
        };
    }
}