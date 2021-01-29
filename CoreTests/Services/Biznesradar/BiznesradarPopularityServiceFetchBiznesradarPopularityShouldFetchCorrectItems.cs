using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private Popularity<PopularityItem> FetchedPopularity => _fixture.FetchedPopularity;
        private PopularityItem[] ExpectedPopularityItems => _fixture.ExpectedPopularityItems;


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact(DisplayName = "BiznesradarPopularityService.FetchBiznesradarPopularity() should return correct data")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_return_correct_data()
        {
            FetchedPopularity.Items.Should().StartWith(ExpectedPopularityItems);
        }
    }
}