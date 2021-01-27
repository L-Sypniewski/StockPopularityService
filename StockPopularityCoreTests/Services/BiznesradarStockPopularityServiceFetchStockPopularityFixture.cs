using System;
using System.IO;
using System.Net.Http;
using HtmlAgilityPack;
using Moq;
using StockPopularityCore.Model;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityCore.Utils;

namespace StockPopularityCoreTests.Services
{
    public class BiznesradarStockPopularityServiceFetchStockPopularityFixture : IDisposable
    {
        public StockPopularity<StockPopularityItem> FetchedStockPopularity { get; }

        public DateTimeOffset ExpectedDateTimeOffset => new DateTimeOffset(2000, 4, 2, 15, 24, 55, new TimeSpan(0, 20, 0));

        private readonly string _htmlTestFilePath = Path.Combine("TestFiles", "BiznesradarRanking.html");


        public BiznesradarStockPopularityServiceFetchStockPopularityFixture()
        {
            var httpClient = new Mock<HttpClient>();
            var dateProvider = new Mock<IDateProvider>();
            dateProvider.Setup(mock => mock.Now)
                        .Returns(ExpectedDateTimeOffset);

            var htmlDocumentReader = new Mock<IHtmlDocumentReader>();

            var testHtmlDocument = CreateTestHtmlDocument(_htmlTestFilePath);
            htmlDocumentReader.Setup(mock => mock.HtmlDocumentFrom(It.IsAny<string>()))
                              .Returns(testHtmlDocument);

            var sut = new BiznesradarStockPopularityService(httpClient.Object, dateProvider.Object, htmlDocumentReader.Object);
            FetchedStockPopularity = sut.FetchBiznesradarStockPopularity().Result;
        }


        private static HtmlDocument CreateTestHtmlDocument(string htmlFilepath)
        {
            var testHtmlDocument = new HtmlDocument();
            var pageSource = File.ReadAllText(htmlFilepath);
            testHtmlDocument.LoadHtml(pageSource);
            return testHtmlDocument;
        }


        public void Dispose()
        {
        }
    }
}