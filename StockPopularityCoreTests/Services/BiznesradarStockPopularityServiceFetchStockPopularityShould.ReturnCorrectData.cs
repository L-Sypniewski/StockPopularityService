using System;
using FluentAssertions;
using StockPopularityCore.Model;
using Xunit;

namespace StockPopularityCoreTests.Services
{
    public class BiznesradarStockPopularityServiceFetchStockPopularityShouldReturnCorrectData
        : IClassFixture<BiznesradarStockPopularityServiceFetchStockPopularityFixture>
    {
        private readonly BiznesradarStockPopularityServiceFetchStockPopularityFixture _fixture;
        private StockPopularity<StockPopularityItem> FetchedStockPopularity => _fixture.FetchedStockPopularity;
        private DateTimeOffset ExpectedDateTimeOffset => _fixture.ExpectedDateTimeOffset;
        private StockPopularityItem[] ExpectedPopularityItems => _fixture.ExpectedPopularityItems;


        public BiznesradarStockPopularityServiceFetchStockPopularityShouldReturnCorrectData(
            BiznesradarStockPopularityServiceFetchStockPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact(DisplayName = "BiznesradarStockPopularityService.FetchStockPopularity() should parse correct amount of items")]
        public void BiznesradarStockPopularityService_FetchStockPopularity_should_parse_correct_amount_of_items()
        {
            const int numberOfItemsInHtml = 200;
            FetchedStockPopularity.Items.Should()
                                  .HaveCount(numberOfItemsInHtml, "that's the amount of companies listed in a test html file");
        }


        [Fact(DisplayName = "BiznesradarStockPopularityService.FetchStockPopularity() should return correct data")]
        public void BiznesradarStockPopularityService_FetchStockPopularity_should_return_correct_data()
        {
            FetchedStockPopularity.Items.Should().StartWith(ExpectedPopularityItems);
        }
    }
}