using System.IO;
using System.Net.Http;
using Core.Model;
using Core.Services.Popularity;
using Core.Utils;
using FluentAssertions;
using HtmlAgilityPack;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectAmountOfItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private readonly Popularity<BiznesradarPopularityItem> _fetchedPopularity;
        private readonly string _htmlTestFilePath = Path.Combine("TestFiles", "BiznesradarRanking.html");


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectAmountOfItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;

            var httpClient = new Mock<HttpClient>();

            var htmlDocumentReader = new Mock<IHtmlDocumentReader>();
            var testHtmlDocument = CreateTestHtmlDocument(_htmlTestFilePath);
            htmlDocumentReader.Setup(mock => mock.HtmlDocumentFrom(It.IsAny<string>()))
                              .Returns(testHtmlDocument);

            var dateProvider = new Mock<IDateProvider>();

            var stockNameFactory = new Mock<IBiznesradarPopularityStockNameFactory>();
            var sut = new BiznesradarPopularityService(httpClient.Object, dateProvider.Object, htmlDocumentReader.Object,
                                                       stockNameFactory.Object);
            _fetchedPopularity = sut.FetchBiznesradarPopularity().Result;
        }


        private static HtmlDocument CreateTestHtmlDocument(string htmlFilepath)
        {
            var testHtmlDocument = new HtmlDocument();
            var pageSource = File.ReadAllText(htmlFilepath);
            testHtmlDocument.LoadHtml(pageSource);
            return testHtmlDocument;
        }


        [Fact(DisplayName =
            "BiznesradarPopularityService.FetchBiznesradarPopularity() should fetch correct amount of items")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_fetch_correct_amount_of_items()
        {
            const int numberOfItemsInHtml = 200;
            _fetchedPopularity.Items.Should()
                              .HaveCount(numberOfItemsInHtml, "that's the amount of companies listed in a test html file");
        }
    }
}