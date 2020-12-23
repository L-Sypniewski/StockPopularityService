using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.Biznesradar
{
    public class BiznesradarPopularityService : IBiznesradarPopularityService
    {
        private readonly HttpClient _httpClient;
        private readonly IDateProvider _dateProvider;
        private readonly ILogger<BiznesradarPopularityService> _logger;
        private const string Uri = "https://www.biznesradar.pl/symbols-rank/";
        private readonly IHtmlDocumentReader _documentReader;


        public BiznesradarPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                            ILogger<BiznesradarPopularityService> logger)
        {
            _httpClient = httpClient;
            _dateProvider = dateProvider;
            _logger = logger;
            _documentReader = new HtmlDocumentReader();
        }


        public async Task<StocksPopularity<StockPopularityItem>> FetchStocksPopularity()
        {
            try
            {
                var pageSource = await _httpClient.GetStringAsync(Uri);
                _logger.LogInformation("Fetched site source from url: {uri}", Uri);

                var currentDate = _dateProvider.Now;
                var tableElements = TableElementsFrom(pageSource);
                _logger.LogInformation("Fetched table elements from page source");

                var stocksPopularityItems = tableElements.Select(PopularityDataFrom)
                                                         .Select(tuple => new StockPopularityItem(tuple.stockName, tuple.rank)).ToArray();

                _logger.LogInformation("Created stock popularity items");

                return new StocksPopularity<StockPopularityItem>(stocksPopularityItems, currentDate);
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
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='qTableFull symbol-rank']");
            return table.SelectNodes("tr").Skip(1);
        }


        private static (int rank, string stockName) PopularityDataFrom(string rowString)
        {
            var stringElements = rowString.Split(" ").Where(x => x != "").ToArray();
            var stockNameContainsTwoCodeNames = stringElements.Length == 11;

            var rank = int.Parse(stringElements.First());

            var stockName = stockNameContainsTwoCodeNames
                ? $"{stringElements[1]} {stringElements[2]}"
                : stringElements[1];

            return ( rank, stockName );
        }
    }
}