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
                CreateStockPopularity("DAX.FUT", 21),
                CreateStockPopularity("TPE", 22, "TAURONPE"),
                CreateStockPopularity("BMX", 23, "BIOMAXIMA"),
                CreateStockPopularity("BLO", 24, "BLOOBER"),
                CreateStockPopularity("PEO", 25, "PEKAO"),
                CreateStockPopularity("PGE", 26),
                CreateStockPopularity("F51", 27, "FARM51"),
                CreateStockPopularity("PGN", 28, "PGNIG"),
                CreateStockPopularity("ITM", 29, "ITMTRADE"),
                CreateStockPopularity("FW20", 30),
                CreateStockPopularity("PKO", 31, "PKOBP"),
                CreateStockPopularity("DJ.FUT", 32),
                CreateStockPopularity("MSZ", 33, "MOSTALZAB"),
                CreateStockPopularity("CIG", 34, "CIGAMES"),
                CreateStockPopularity("USD/PLN", 35, "1:1 - dolar/złoty"),
                CreateStockPopularity("ENA", 36, "ENEA"),
                CreateStockPopularity("ULG", 37, "ULTGAMES"),
                CreateStockPopularity("SP500", 38),
                CreateStockPopularity("ASB", 39, "ASBIS"),
                CreateStockPopularity("ALR", 40, "ALIOR"),
                CreateStockPopularity("TEN", 41, "TSGAMES"),
                CreateStockPopularity("MRB", 42, "MIRBUD"),
                CreateStockPopularity("ATT", 43, "GRUPAAZOTY"),
                CreateStockPopularity("GTS", 44, "GEOTRANS"),
                CreateStockPopularity("PDZ", 45, "PRAIRIE"),
                CreateStockPopularity("MIL", 46, "MILLENNIUM"),
                CreateStockPopularity("DNP", 47, "DINOPL"),
                CreateStockPopularity("PBG", 48),
                CreateStockPopularity("ATS", 49, "ATLANTIS"),
                CreateStockPopularity("TRK", 50, "TRAKCJA"),
                CreateStockPopularity("OAT", 51),
                CreateStockPopularity("MLS", 52, "MLSYSTEM"),
                CreateStockPopularity("FMF", 53, "FAMUR"),
                CreateStockPopularity("MVP", 54, "MARVIPOL"),
                CreateStockPopularity("TOA", 55, "TOYA"),
                CreateStockPopularity("FOR", 56, "FOREVEREN"),
                CreateStockPopularity("BIO", 57, "BIOTON"),
                CreateStockPopularity("LWB", 58, "BOGDANKA"),
                CreateStockPopularity("PCF", 59, "PCFGROUP"),
                CreateStockPopularity("UNT", 60, "UNIMOT"),
                CreateStockPopularity("WIG", 61),
                CreateStockPopularity("Złoto", 62, "Gold Spot"),
                CreateStockPopularity("ECL", 63, "EASYCALL"),
                CreateStockPopularity("CLN", 64, "CLNPHARMA"),
                CreateStockPopularity("CIE", 65, "CIECH"),
                CreateStockPopularity("Ropa Brent", 66, "Brent Crude Oil Spot"),
                CreateStockPopularity("PKP", 67, "PKPCARGO"),
                CreateStockPopularity("EUR", 68, "EUROCASH"),
                CreateStockPopularity("GIF", 69, "GAMFACTOR"),
                CreateStockPopularity("INC", 70),
                CreateStockPopularity("PLW", 71, "PLAYWAY"),
                CreateStockPopularity("AST", 72, "ASTARTA"),
                CreateStockPopularity("Gaz ziemny", 73, "Natural Gas Futures"),
                CreateStockPopularity("SBE", 74, "SOFTBLUE"),
                CreateStockPopularity("Srebro", 75, "Silver Futures"),
                CreateStockPopularity("SIM", 76, "SIMFABRIC"),
                CreateStockPopularity("TIM", 77),
                CreateStockPopularity("PCX", 78, "PCCEXOL"),
                CreateStockPopularity("GX1", 79, "GENXONE"),
                CreateStockPopularity("Ropa naftowa", 80, "WTI Light Crude Oil Spot"),
                CreateStockPopularity("DAX", 81),
                CreateStockPopularity("HRP", 82, "HARPER"),
                CreateStockPopularity("KVT", 83, "KRVITAMIN"),
                CreateStockPopularity("SNX", 84, "SUNEX"),
                CreateStockPopularity("EXC", 85, "EXCELLENC"),
                CreateStockPopularity("MBK", 86, "MBANK"),
                CreateStockPopularity("11B", 87, "11BIT"),
                CreateStockPopularity("IDH", 88),
                CreateStockPopularity("OZE", 89, "OZECAPITAL"),
                CreateStockPopularity("EUR/PLN", 90, "1:1 - euro/złoty"),
                CreateStockPopularity("GLC", 91, "GLCOSMED"),
                CreateStockPopularity("MBR", 92, "MOBRUK"),
                CreateStockPopularity("OPL", 93, "ORANGEPL"),
                CreateStockPopularity("ASA", 94, "APIS"),
                CreateStockPopularity("CRJ", 95, "CREEPYJAR"),
                CreateStockPopularity("KER", 96, "KERNEL"),
                CreateStockPopularity("SNT", 97, "SYNEKTIK"),
                CreateStockPopularity("KTY", 98, "KETY"),
                CreateStockPopularity("RLP", 99, "RELPOL"),
                CreateStockPopularity("MOV", 100, "MOVIEGAMES"),
                CreateStockPopularity("AWM", 101, "AIRWAY"),
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