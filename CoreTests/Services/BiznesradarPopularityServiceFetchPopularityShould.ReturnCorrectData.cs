using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Core.Model;
using Xunit;

namespace CoreTests.Services
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularity
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<PopularityItem> FetchedPopularity => _fixture.FetchedPopularity;
        private DateTimeOffset ExpectedDateTimeOffset => _fixture.ExpectedDateTimeOffset;
        private PopularityItem[] ExpectedPopularityItems => _fixture.ExpectedPopularityItems;


        public BiznesradarPopularityServiceFetchBiznesradarPopularity(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should parse correct amount of items")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_parse_correct_amount_of_items()
        {
            const int numberOfItemsInHtml = 200;
            FetchedPopularity.Items.Should()
                                  .HaveCount(numberOfItemsInHtml, "that's the amount of companies listed in a test html file");
        }


        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly parse StockNames with codename and long name")]
        [MemberData(nameof(CodenameAndLongNameData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_parse_StockNames_with_codename_and_long_name(
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

        [Theory(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should correctly parse StockNames with codename only")]
        [MemberData(nameof(CodenameOnlyData))]
        public void
            BiznesradarPopularityService_FetchBiznesradarPopularity_should_correctly_parse_StockNames_with_codename_only(
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


        [Fact(DisplayName = "BiznesradarPopularityService.FetchBiznesradarPopularity() should return correct data")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_return_correct_data()
        {
            FetchedPopularity.Items.Should().StartWith(ExpectedPopularityItems);
        }
    }
}