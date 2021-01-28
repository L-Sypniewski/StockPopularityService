using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using HtmlAgilityPack;
using Moq;
using StockPopularityCore.Model;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityCore.Utils;

namespace StockPopularityCoreTests.Services
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class BiznesradarStockPopularityServiceFetchStockPopularityFixture : IDisposable
    {
        public StockPopularity<StockPopularityItem> FetchedStockPopularity { get; }
        public StockPopularityItem[] ExpectedPopularityItems { get; }
        public StockPopularity<StockPopularityItem> ExpectedStockPopularity { get; }

        public DateTimeOffset ExpectedDateTimeOffset => new DateTimeOffset(2000, 4, 2, 15, 24, 55, new TimeSpan(0, 20, 0));

        private readonly string _htmlTestFilePath = Path.Combine("TestFiles", "BiznesradarRanking.html");


        public BiznesradarStockPopularityServiceFetchStockPopularityFixture()
        {
            var httpClient = new Mock<HttpClient>();
            var htmlDocumentReader = new Mock<IHtmlDocumentReader>();

            var dateProvider = new Mock<IDateProvider>();
            dateProvider.Setup(mock => mock.Now)
                        .Returns(ExpectedDateTimeOffset);

            var testHtmlDocument = CreateTestHtmlDocument(_htmlTestFilePath);
            htmlDocumentReader.Setup(mock => mock.HtmlDocumentFrom(It.IsAny<string>()))
                              .Returns(testHtmlDocument);

            var sut = new BiznesradarStockPopularityService(httpClient.Object, dateProvider.Object, htmlDocumentReader.Object);
            FetchedStockPopularity = sut.FetchBiznesradarStockPopularity().Result;
            ExpectedPopularityItems = CreateExpectedPopularityItems();
            ExpectedStockPopularity = CreateExpectedStockPopularity();
        }


        private static HtmlDocument CreateTestHtmlDocument(string htmlFilepath)
        {
            var testHtmlDocument = new HtmlDocument();
            var pageSource = File.ReadAllText(htmlFilepath);
            testHtmlDocument.LoadHtml(pageSource);
            return testHtmlDocument;
        }


        private StockPopularity<StockPopularityItem> CreateExpectedStockPopularity()
        {
            return new StockPopularity<StockPopularityItem>(CreateExpectedPopularityItems(), ExpectedDateTimeOffset);
        }


        private static StockPopularityItem[] CreateExpectedPopularityItems()
        {
            return new[]
            {
                CreateStockPopularity("CDR", 1, "CDPROJEKT"),
                CreateStockPopularity("BML", 2, "BIOMEDLUB"),
                CreateStockPopularity("MRC", 3, "MERCATOR"),
                CreateStockPopularity("ALE", 4, "ALLEGRO"),
                CreateStockPopularity("SEN", 5, "SERINUS"),
                CreateStockPopularity("JSW", 6),
                CreateStockPopularity("PXM", 7, "POLIMEXMS"),
                CreateStockPopularity("BRU", 8, "BORUTA"),
                CreateStockPopularity("PKN", 9, "PKNORLEN"),
                CreateStockPopularity("GRN", 10, "GRODNO"),
                CreateStockPopularity("WIG20", 11),
                CreateStockPopularity("KGH", 12, "KGHM"),
                CreateStockPopularity("XTB", 13),
                CreateStockPopularity("IGN", 14, "INNOGENE"),
                CreateStockPopularity("4MS", 15, "4MASS"),
                CreateStockPopularity("PZU", 16),
                CreateStockPopularity("RFK", 17, "RAFAKO"),
                CreateStockPopularity("CLC", 18, "COLUMBUS"),
                CreateStockPopularity("LTS", 19, "LOTOS"),
                CreateStockPopularity("CCC", 20),
                CreateStockPopularity("DAX.FUT", 21)
                // CreateStockPopularity("TPE", "TAURONPE"), 22),
                // CreateStockPopularity("BMX", "BIOMAXIMA"), 23),
            };
        }


        private static StockPopularityItem CreateStockPopularity(string codename, int rank, string? longName = null)
        {
            return new StockPopularityItem(new StockName(codename, longName), rank);
        }


        public void Dispose()
        {
        }
    }
}