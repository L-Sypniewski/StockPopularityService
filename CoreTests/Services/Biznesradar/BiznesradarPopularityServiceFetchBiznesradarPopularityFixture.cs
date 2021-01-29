using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using Core.Model;
using Core.Services.Popularity;
using Core.Utils;
using HtmlAgilityPack;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    [CollectionDefinition("BiznesradarPopularityService.FetchBiznesradarPopularity()")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityCollection : ICollectionFixture<
        BiznesradarPopularityServiceFetchBiznesradarPopularityFixture>
    {
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class BiznesradarPopularityServiceFetchBiznesradarPopularityFixture : IDisposable
    {
        public Popularity<PopularityItem> FetchedPopularity { get; }
        public PopularityItem[] ExpectedPopularityItems { get; }
        public Popularity<PopularityItem> ExpectedPopularity { get; }

        public DateTimeOffset ExpectedDateTimeOffset => new DateTimeOffset(2000, 4, 2, 15, 24, 55, new TimeSpan(0, 20, 0));

        private readonly string _htmlTestFilePath = Path.Combine("TestFiles", "BiznesradarRanking.html");


        public BiznesradarPopularityServiceFetchBiznesradarPopularityFixture()
        {
            var httpClient = new Mock<HttpClient>();
            var htmlDocumentReader = new Mock<IHtmlDocumentReader>();

            var dateProvider = new Mock<IDateProvider>();
            dateProvider.Setup(mock => mock.Now)
                        .Returns(ExpectedDateTimeOffset);

            var testHtmlDocument = CreateTestHtmlDocument(_htmlTestFilePath);
            htmlDocumentReader.Setup(mock => mock.HtmlDocumentFrom(It.IsAny<string>()))
                              .Returns(testHtmlDocument);

            var sut = new BiznesradarPopularityService(httpClient.Object, dateProvider.Object, htmlDocumentReader.Object);
            FetchedPopularity = sut.FetchBiznesradarPopularity().Result;
            ExpectedPopularityItems = CreateExpectedPopularityItems();
            ExpectedPopularity = CreateExpectedPopularity();
        }


        private static HtmlDocument CreateTestHtmlDocument(string htmlFilepath)
        {
            var testHtmlDocument = new HtmlDocument();
            var pageSource = File.ReadAllText(htmlFilepath);
            testHtmlDocument.LoadHtml(pageSource);
            return testHtmlDocument;
        }


        private Popularity<PopularityItem> CreateExpectedPopularity()
        {
            return new Popularity<PopularityItem>(CreateExpectedPopularityItems(), ExpectedDateTimeOffset);
        }


        private static PopularityItem[] CreateExpectedPopularityItems()
        {
            return new[]
            {
                CreatePopularityItem("CDR", 1, "CDPROJEKT"),
                CreatePopularityItem("BML", 2, "BIOMEDLUB"),
                CreatePopularityItem("MRC", 3, "MERCATOR"),
                CreatePopularityItem("ALE", 4, "ALLEGRO"),
                CreatePopularityItem("SEN", 5, "SERINUS"),
                CreatePopularityItem("JSW", 6),
                CreatePopularityItem("PXM", 7, "POLIMEXMS"),
                CreatePopularityItem("BRU", 8, "BORUTA"),
                CreatePopularityItem("PKN", 9, "PKNORLEN"),
                CreatePopularityItem("GRN", 10, "GRODNO"),
                CreatePopularityItem("WIG20", 11),
                CreatePopularityItem("KGH", 12, "KGHM"),
                CreatePopularityItem("XTB", 13),
                CreatePopularityItem("IGN", 14, "INNOGENE"),
                CreatePopularityItem("4MS", 15, "4MASS"),
                CreatePopularityItem("PZU", 16),
                CreatePopularityItem("RFK", 17, "RAFAKO"),
                CreatePopularityItem("CLC", 18, "COLUMBUS"),
                CreatePopularityItem("LTS", 19, "LOTOS"),
                CreatePopularityItem("CCC", 20),
                CreatePopularityItem("DAX.FUT", 21),
                CreatePopularityItem("TPE", 22, "TAURONPE"),
                CreatePopularityItem("BMX", 23, "BIOMAXIMA"),
                CreatePopularityItem("BLO", 24, "BLOOBER"),
                CreatePopularityItem("PEO", 25, "PEKAO"),
                CreatePopularityItem("PGE", 26),
                CreatePopularityItem("F51", 27, "FARM51"),
                CreatePopularityItem("PGN", 28, "PGNIG"),
                CreatePopularityItem("ITM", 29, "ITMTRADE"),
                CreatePopularityItem("FW20", 30),
                CreatePopularityItem("PKO", 31, "PKOBP"),
                CreatePopularityItem("DJ.FUT", 32),
                CreatePopularityItem("MSZ", 33, "MOSTALZAB"),
                CreatePopularityItem("CIG", 34, "CIGAMES"),
                CreatePopularityItem("USD/PLN", 35, "1:1 - dolar/złoty"),
                CreatePopularityItem("ENA", 36, "ENEA"),
                CreatePopularityItem("ULG", 37, "ULTGAMES"),
                CreatePopularityItem("SP500", 38),
                CreatePopularityItem("ASB", 39, "ASBIS"),
                CreatePopularityItem("ALR", 40, "ALIOR"),
                CreatePopularityItem("TEN", 41, "TSGAMES"),
                CreatePopularityItem("MRB", 42, "MIRBUD"),
                CreatePopularityItem("ATT", 43, "GRUPAAZOTY"),
                CreatePopularityItem("GTS", 44, "GEOTRANS"),
                CreatePopularityItem("PDZ", 45, "PRAIRIE"),
                CreatePopularityItem("MIL", 46, "MILLENNIUM"),
                CreatePopularityItem("DNP", 47, "DINOPL"),
                CreatePopularityItem("PBG", 48),
                CreatePopularityItem("ATS", 49, "ATLANTIS"),
                CreatePopularityItem("TRK", 50, "TRAKCJA"),
                CreatePopularityItem("OAT", 51),
                CreatePopularityItem("MLS", 52, "MLSYSTEM"),
                CreatePopularityItem("FMF", 53, "FAMUR"),
                CreatePopularityItem("MVP", 54, "MARVIPOL"),
                CreatePopularityItem("TOA", 55, "TOYA"),
                CreatePopularityItem("FOR", 56, "FOREVEREN"),
                CreatePopularityItem("BIO", 57, "BIOTON"),
                CreatePopularityItem("LWB", 58, "BOGDANKA"),
                CreatePopularityItem("PCF", 59, "PCFGROUP"),
                CreatePopularityItem("UNT", 60, "UNIMOT"),
                CreatePopularityItem("WIG", 61),
                CreatePopularityItem("Złoto", 62, "Gold Spot"),
                CreatePopularityItem("ECL", 63, "EASYCALL"),
                CreatePopularityItem("CLN", 64, "CLNPHARMA"),
                CreatePopularityItem("CIE", 65, "CIECH"),
                CreatePopularityItem("Ropa Brent", 66, "Brent Crude Oil Spot"),
                CreatePopularityItem("PKP", 67, "PKPCARGO"),
                CreatePopularityItem("EUR", 68, "EUROCASH"),
                CreatePopularityItem("GIF", 69, "GAMFACTOR"),
                CreatePopularityItem("INC", 70),
                CreatePopularityItem("PLW", 71, "PLAYWAY"),
                CreatePopularityItem("AST", 72, "ASTARTA"),
                CreatePopularityItem("Gaz ziemny", 73, "Natural Gas Futures"),
                CreatePopularityItem("SBE", 74, "SOFTBLUE"),
                CreatePopularityItem("Srebro", 75, "Silver Futures"),
                CreatePopularityItem("SIM", 76, "SIMFABRIC"),
                CreatePopularityItem("TIM", 77),
                CreatePopularityItem("PCX", 78, "PCCEXOL"),
                CreatePopularityItem("GX1", 79, "GENXONE"),
                CreatePopularityItem("Ropa naftowa", 80, "WTI Light Crude Oil Spot"),
                CreatePopularityItem("DAX", 81),
                CreatePopularityItem("HRP", 82, "HARPER"),
                CreatePopularityItem("KVT", 83, "KRVITAMIN"),
                CreatePopularityItem("SNX", 84, "SUNEX"),
                CreatePopularityItem("EXC", 85, "EXCELLENC"),
                CreatePopularityItem("MBK", 86, "MBANK"),
                CreatePopularityItem("11B", 87, "11BIT"),
                CreatePopularityItem("IDH", 88),
                CreatePopularityItem("OZE", 89, "OZECAPITAL"),
                CreatePopularityItem("EUR/PLN", 90, "1:1 - euro/złoty"),
                CreatePopularityItem("GLC", 91, "GLCOSMED"),
                CreatePopularityItem("MBR", 92, "MOBRUK"),
                CreatePopularityItem("OPL", 93, "ORANGEPL"),
                CreatePopularityItem("ASA", 94, "APIS"),
                CreatePopularityItem("CRJ", 95, "CREEPYJAR"),
                CreatePopularityItem("KER", 96, "KERNEL"),
                CreatePopularityItem("SNT", 97, "SYNEKTIK"),
                CreatePopularityItem("KTY", 98, "KETY"),
                CreatePopularityItem("RLP", 99, "RELPOL"),
                CreatePopularityItem("MOV", 100, "MOVIEGAMES"),
                CreatePopularityItem("AWM", 101, "AIRWAY"),
            };
        }


        private static PopularityItem CreatePopularityItem(string codename, int rank, string? longName = null)
        {
            return new PopularityItem(new StockName(codename, longName), rank);
        }


        public void Dispose()
        {
        }
    }
}