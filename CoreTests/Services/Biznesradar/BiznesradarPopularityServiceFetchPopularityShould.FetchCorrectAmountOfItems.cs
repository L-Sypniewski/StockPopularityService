using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectAmountOfItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<BiznesradarPopularityItem> FetchedPopularity => _fixture.FetchedPopularity;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectAmountOfItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should fetch correct amount of items")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_fetch_correct_amount_of_items()
        {
            const int numberOfItemsInHtml = 200;
            FetchedPopularity.Items.Should()
                             .HaveCount(numberOfItemsInHtml, "that's the amount of companies listed in a test html file");
        }
    }
}