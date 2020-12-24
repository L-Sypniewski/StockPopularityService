using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Services.Biznesradar;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.Bankier
{
    public class BankierPopularityService : IBankierPopularityService
    {
        private readonly HttpClient _httpClient;
        private readonly IDateProvider _dateProvider;
        private readonly ILogger<BiznesradarPopularityService> _logger;
        private readonly HtmlDocumentReader _documentReader;
        private const string Uri = "https://www.bankier.pl/gielda/notowania/ranking-popularnosci";


        public BankierPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                        ILogger<BiznesradarPopularityService> logger)
        {
            _httpClient = httpClient;
            _dateProvider = dateProvider;
            _logger = logger;
            _documentReader = new HtmlDocumentReader();
        }


        public async Task<StocksPopularity<BankierStockPopularityItem>> FetchStocksPopularity()
        {
            try
            {
                var pageSource = await _httpClient.GetStringAsync(Uri);
                _logger.LogInformation("Fetched site source from url: {uri}", Uri);

                var currentDate = _dateProvider.Now;
                var tableElements = TableElementsFrom(pageSource);
                _logger.LogInformation("Fetched table elements from page source");

                var stocksPopularityItems = tableElements.Select(PopularityDataFrom)
                                                         .Select(tuple => new BankierStockPopularityItem(
                                                                     tuple.stockName, tuple.rank, tuple.last30DaysPostsCount));

                _logger.LogInformation("Created stock popularity items");

                return new StocksPopularity<BankierStockPopularityItem>(stocksPopularityItems, currentDate);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    "An exception has been throw when fetching stocks popularity from Biznesradar. Exception message: {message}",
                    exception.Message);
                throw;
            }
        }


        private IEnumerable<string> TableElementsFrom(string pageSource)
        {
            var document = _documentReader.HtmlDocumentFrom(pageSource);
            var rows = TableRowsFrom(document);
            return rows.Select(row => row.InnerText.WithoutNewLineChars())
                       .Where(rowWithoutNewLineChars => !string.IsNullOrWhiteSpace(rowWithoutNewLineChars));
        }


        private static IEnumerable<HtmlNode> TableRowsFrom(HtmlDocument htmlDocument)
        {
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table");
            var rows = table.SelectNodes("//tr").Skip(1).ToArray();
            return rows;
        }


        private static (int rank, string stockName, int last30DaysPostsCount) PopularityDataFrom(string rowString)
        {
            var stringElements = rowString.Split(" ").Where(x => x != "").ToArray();

            var rank = int.Parse(stringElements.First());
            var longName = stringElements[1];
            var codename = stringElements[2];
            var last30DaysPostsCount = int.Parse(stringElements[3]);

            return ( rank, $"{longName} ({codename})", last30DaysPostsCount );
        }
    }
}