using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Core.Model;
using Core.Services.Popularity;
using Core.Utils;
using CoreTests.Services.Biznesradar;
using FluentAssertions;
using HtmlAgilityPack;
using Moq;
using Xunit;

namespace CoreTests.IntegrationTests
{
    [Collection("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectItems
    {
        private readonly BiznesradarPopularityServiceFetchBiznesradarPopularityFixture _fixture;
        private readonly Popularity<BiznesradarPopularityItem> _fetchedPopularity;
        private IEnumerable<BiznesradarPopularityItem> ExpectedPopularityItems => _fixture.ExpectedPopularityItems;
        private readonly string _htmlTestFilePath = Path.Combine("TestFiles", "BiznesradarRanking.html");


        private static HtmlDocument CreateTestHtmlDocument(string htmlFilepath)
        {
            var testHtmlDocument = new HtmlDocument();
            var pageSource = File.ReadAllText(htmlFilepath);
            testHtmlDocument.LoadHtml(pageSource);
            return testHtmlDocument;
        }


        public BiznesradarPopularityServiceFetchBiznesradarPopularityShouldFetchCorrectItems(
            BiznesradarPopularityServiceFetchBiznesradarPopularityFixture fixture)
        {
            _fixture = fixture;

            var httpClient = new Mock<HttpClient>();

            var htmlDocumentReader = new Mock<IHtmlDocumentReader>();
            var testHtmlDocument = CreateTestHtmlDocument(_htmlTestFilePath);
            htmlDocumentReader.Setup(mock => mock.HtmlDocumentFrom(It.IsAny<string>()))
                              .Returns(testHtmlDocument);

            var dateProvider = new Mock<IDateProvider>();

            var typeFactory = new PopularityItemTypeFactory();
            var stockNameFactory = new BiznesradarPopularityStockNameFactory(typeFactory);
            var sut = new BiznesradarPopularityService(httpClient.Object, dateProvider.Object, htmlDocumentReader.Object,
                                                       stockNameFactory, typeFactory);
            _fetchedPopularity = sut.FetchBiznesradarPopularity().Result;
        }


        [Fact(DisplayName = "BiznesradarPopularityService.FetchBiznesradarPopularity() should return correct data")]
        public void BiznesradarPopularityService_FetchBiznesradarPopularity_should_return_correct_data()
        {
            _fetchedPopularity.Items.Should().StartWith(ExpectedPopularityItems);
        }
    }
}