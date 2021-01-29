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
        public BiznesradarPopularityItem[] ExpectedPopularityItems { get; }
        public Popularity<BiznesradarPopularityItem> ExpectedPopularity { get; }

        public static DateTimeOffset ExpectedDateTimeOffset => new DateTimeOffset(2000, 4, 2, 15, 24, 55, new TimeSpan(0, 20, 0));


        public BiznesradarPopularityServiceFetchBiznesradarPopularityFixture()
        {
            ExpectedPopularityItems = CreateExpectedPopularityItems();
            ExpectedPopularity = CreateExpectedPopularity();
        }


        private Popularity<BiznesradarPopularityItem> CreateExpectedPopularity()
        {
            return new Popularity<BiznesradarPopularityItem>(CreateExpectedPopularityItems(), ExpectedDateTimeOffset);
        }


        private static BiznesradarPopularityItem[] CreateExpectedPopularityItems()
        {
            return new[]
            {
                CreatePopularityItem("CDR", 1, PopularityItemType.Stock, "CDPROJEKT"),
                CreatePopularityItem("BML", 2, PopularityItemType.Stock, "BIOMEDLUB"),
                CreatePopularityItem("MRC", 3, PopularityItemType.Stock, "MERCATOR"),
                CreatePopularityItem("ALE", 4, PopularityItemType.Stock, "ALLEGRO"),
                CreatePopularityItem("SEN", 5, PopularityItemType.Stock, "SERINUS"),
                CreatePopularityItem("JSW", 6, PopularityItemType.Stock),
                CreatePopularityItem("PXM", 7, PopularityItemType.Stock, "POLIMEXMS"),
                CreatePopularityItem("BRU", 8, PopularityItemType.Stock, "BORUTA"),
                CreatePopularityItem("PKN", 9, PopularityItemType.Stock, "PKNORLEN"),
                CreatePopularityItem("GRN", 10, PopularityItemType.Stock, "GRODNO"),
                CreatePopularityItem("WIG20", 11, PopularityItemType.Index),
                CreatePopularityItem("KGH", 12, PopularityItemType.Stock, "KGHM"),
                CreatePopularityItem("XTB", 13, PopularityItemType.Stock),
                CreatePopularityItem("IGN", 14, PopularityItemType.Stock, "INNOGENE"),
                CreatePopularityItem("4MS", 15, PopularityItemType.Stock, "4MASS"),
                CreatePopularityItem("PZU", 16, PopularityItemType.Stock),
                CreatePopularityItem("RFK", 17, PopularityItemType.Stock, "RAFAKO"),
                CreatePopularityItem("CLC", 18, PopularityItemType.Stock, "COLUMBUS"),
                CreatePopularityItem("LTS", 19, PopularityItemType.Stock, "LOTOS"),
                CreatePopularityItem("CCC", 20, PopularityItemType.Stock),
                CreatePopularityItem("DAX.FUT", 21, PopularityItemType.Index),
                CreatePopularityItem("TPE", 22, PopularityItemType.Stock, "TAURONPE"),
                CreatePopularityItem("BMX", 23, PopularityItemType.Stock, "BIOMAXIMA"),
                CreatePopularityItem("BLO", 24, PopularityItemType.Stock, "BLOOBER"),
                CreatePopularityItem("PEO", 25, PopularityItemType.Stock, "PEKAO"),
                CreatePopularityItem("PGE", 26, PopularityItemType.Stock),
                CreatePopularityItem("F51", 27, PopularityItemType.Stock, "FARM51"),
                CreatePopularityItem("PGN", 28, PopularityItemType.Stock, "PGNIG"),
                CreatePopularityItem("ITM", 29, PopularityItemType.Stock, "ITMTRADE"),
                CreatePopularityItem("FW20", 30, PopularityItemType.Index),
                CreatePopularityItem("PKO", 31, PopularityItemType.Stock, "PKOBP"),
                CreatePopularityItem("DJ.FUT", 32, PopularityItemType.Index),
                CreatePopularityItem("MSZ", 33, PopularityItemType.Stock, "MOSTALZAB"),
                CreatePopularityItem("CIG", 34, PopularityItemType.Stock, "CIGAMES"),
                CreatePopularityItem("USD/PLN", 35, PopularityItemType.Currency, "1:1 - dolar/złoty"),
                CreatePopularityItem("ENA", 36, PopularityItemType.Stock, "ENEA"),
                CreatePopularityItem("ULG", 37, PopularityItemType.Stock, "ULTGAMES"),
                CreatePopularityItem("SP500", 38, PopularityItemType.Index),
                CreatePopularityItem("ASB", 39, PopularityItemType.Stock, "ASBIS"),
                CreatePopularityItem("ALR", 40, PopularityItemType.Stock, "ALIOR"),
                CreatePopularityItem("TEN", 41, PopularityItemType.Stock, "TSGAMES"),
                CreatePopularityItem("MRB", 42, PopularityItemType.Stock, "MIRBUD"),
                CreatePopularityItem("ATT", 43, PopularityItemType.Stock, "GRUPAAZOTY"),
                CreatePopularityItem("GTS", 44, PopularityItemType.Stock, "GEOTRANS"),
                CreatePopularityItem("PDZ", 45, PopularityItemType.Stock, "PRAIRIE"),
                CreatePopularityItem("MIL", 46, PopularityItemType.Stock, "MILLENNIUM"),
                CreatePopularityItem("DNP", 47, PopularityItemType.Stock, "DINOPL"),
                CreatePopularityItem("PBG", 48, PopularityItemType.Stock),
                CreatePopularityItem("ATS", 49, PopularityItemType.Stock, "ATLANTIS"),
                CreatePopularityItem("TRK", 50, PopularityItemType.Stock, "TRAKCJA"),
                CreatePopularityItem("OAT", 51, PopularityItemType.Stock),
                CreatePopularityItem("MLS", 52, PopularityItemType.Stock, "MLSYSTEM"),
                CreatePopularityItem("FMF", 53, PopularityItemType.Stock, "FAMUR"),
                CreatePopularityItem("MVP", 54, PopularityItemType.Stock, "MARVIPOL"),
                CreatePopularityItem("TOA", 55, PopularityItemType.Stock, "TOYA"),
                CreatePopularityItem("FOR", 56, PopularityItemType.Stock, "FOREVEREN"),
                CreatePopularityItem("BIO", 57, PopularityItemType.Stock, "BIOTON"),
                CreatePopularityItem("LWB", 58, PopularityItemType.Stock, "BOGDANKA"),
                CreatePopularityItem("PCF", 59, PopularityItemType.Stock, "PCFGROUP"),
                CreatePopularityItem("UNT", 60, PopularityItemType.Stock, "UNIMOT"),
                CreatePopularityItem("WIG", 61, PopularityItemType.Index),
                CreatePopularityItem("Złoto", 62, PopularityItemType.Commodity, "Gold Spot"),
                CreatePopularityItem("ECL", 63, PopularityItemType.Stock, "EASYCALL"),
                CreatePopularityItem("CLN", 64, PopularityItemType.Stock, "CLNPHARMA"),
                CreatePopularityItem("CIE", 65, PopularityItemType.Stock, "CIECH"),
                CreatePopularityItem("Ropa Brent", 66, PopularityItemType.Commodity, "Brent Crude Oil Spot"),
                CreatePopularityItem("PKP", 67, PopularityItemType.Stock, "PKPCARGO"),
                CreatePopularityItem("EUR", 68, PopularityItemType.Stock, "EUROCASH"),
                CreatePopularityItem("GIF", 69, PopularityItemType.Stock, "GAMFACTOR"),
                CreatePopularityItem("INC", 70, PopularityItemType.Stock),
                CreatePopularityItem("PLW", 71, PopularityItemType.Stock, "PLAYWAY"),
                CreatePopularityItem("AST", 72, PopularityItemType.Stock, "ASTARTA"),
                CreatePopularityItem("Gaz ziemny", 73, PopularityItemType.Commodity, "Natural Gas Futures"),
                CreatePopularityItem("SBE", 74, PopularityItemType.Stock, "SOFTBLUE"),
                CreatePopularityItem("Srebro", 75, PopularityItemType.Commodity, "Silver Futures"),
                CreatePopularityItem("SIM", 76, PopularityItemType.Stock, "SIMFABRIC"),
                CreatePopularityItem("TIM", 77, PopularityItemType.Stock),
                CreatePopularityItem("PCX", 78, PopularityItemType.Stock, "PCCEXOL"),
                CreatePopularityItem("GX1", 79, PopularityItemType.Stock, "GENXONE"),
                CreatePopularityItem("Ropa naftowa", 80, PopularityItemType.Commodity, "WTI Light Crude Oil Spot"),
                CreatePopularityItem("DAX", 81, PopularityItemType.Index),
                CreatePopularityItem("HRP", 82, PopularityItemType.Stock, "HARPER"),
                CreatePopularityItem("KVT", 83, PopularityItemType.Stock, "KRVITAMIN"),
                CreatePopularityItem("SNX", 84, PopularityItemType.Stock, "SUNEX"),
                CreatePopularityItem("EXC", 85, PopularityItemType.Stock, "EXCELLENC"),
                CreatePopularityItem("MBK", 86, PopularityItemType.Stock, "MBANK"),
                CreatePopularityItem("11B", 87, PopularityItemType.Stock, "11BIT"),
                CreatePopularityItem("IDH", 88, PopularityItemType.Stock),
                CreatePopularityItem("OZE", 89, PopularityItemType.Stock, "OZECAPITAL"),
                CreatePopularityItem("EUR/PLN", 90, PopularityItemType.Currency, "1:1 - euro/złoty"),
                CreatePopularityItem("GLC", 91, PopularityItemType.Stock, "GLCOSMED"),
                CreatePopularityItem("MBR", 92, PopularityItemType.Stock, "MOBRUK"),
                CreatePopularityItem("OPL", 93, PopularityItemType.Stock, "ORANGEPL"),
                CreatePopularityItem("ASA", 94, PopularityItemType.Stock, "APIS"),
                CreatePopularityItem("CRJ", 95, PopularityItemType.Stock, "CREEPYJAR"),
                CreatePopularityItem("KER", 96, PopularityItemType.Stock, "KERNEL"),
                CreatePopularityItem("SNT", 97, PopularityItemType.Stock, "SYNEKTIK"),
                CreatePopularityItem("KTY", 98, PopularityItemType.Stock, "KETY"),
                CreatePopularityItem("RLP", 99, PopularityItemType.Stock, "RELPOL"),
                CreatePopularityItem("MOV", 100, PopularityItemType.Stock, "MOVIEGAMES"),
                CreatePopularityItem("AWM", 101, PopularityItemType.Stock, "AIRWAY")
            };
        }


        private static BiznesradarPopularityItem CreatePopularityItem(string codename, int rank, PopularityItemType? type = null,
                                                                      string? longName = null)
        {
            return new BiznesradarPopularityItem(new StockName(codename, longName), rank, type);
        }


        public void Dispose()
        {
        }
    }
}