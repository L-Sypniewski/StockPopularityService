using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Model;
using Core.Utils;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Core.Services.Popularity
{
    public abstract class AbstractPopularityService<TPopularityItem> : IPopularityService
        where TPopularityItem : IPopularityItem
    {
        private readonly HttpClient _httpClient;
        private readonly IDateProvider _dateProvider;
        private readonly ILogger<AbstractPopularityService<TPopularityItem>> _logger;
        private readonly IHtmlDocumentReader _documentReader;

        protected abstract string Uri { get; }
        protected abstract string TableXpath { get; }
        protected abstract string TableRowsXpath { get; }
        protected abstract string WebsiteDisplayName { get; }


        protected AbstractPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                                 IHtmlDocumentReader htmlDocumentReader,
                                                 ILogger<AbstractPopularityService<TPopularityItem>>? logger)
        {
            _httpClient = httpClient;
            _dateProvider = dateProvider;
            _documentReader = htmlDocumentReader;
            _logger = logger ?? NullLogger<AbstractPopularityService<TPopularityItem>>.Instance;
        }


        public async Task<Popularity<IPopularityItem>> FetchPopularity()
        {
            try
            {
                var pageSource = await _httpClient.GetStringAsync(Uri);
                _logger.LogInformation("Fetched site source from url: {Uri}", Uri);

                var currentDate = _dateProvider.Now;
                var tableElements = TableElementsFrom(pageSource);
                _logger.LogInformation("Fetched table elements from page source");

                var stocksPopularityItems = tableElements.Select(PopularityItemFrom).ToArray();

                _logger.LogInformation("Created stock popularity items from {WebsiteName} data", WebsiteDisplayName);

                return new Popularity<IPopularityItem>(stocksPopularityItems.Cast<IPopularityItem>(), currentDate);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                                 "An exception has been throw when fetching stocks popularity from {WebsiteName}. Exception message:",
                                 WebsiteDisplayName);
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


        private IEnumerable<HtmlNode> TableRowsFrom(HtmlDocument htmlDocument)
        {
            var table = htmlDocument.DocumentNode.SelectSingleNode(TableXpath);
            var rows = table.SelectNodes(TableRowsXpath).Skip(1).ToArray();
            return rows;
        }


        protected abstract TPopularityItem PopularityItemFrom(string rowString);


        protected async Task<Popularity<TPopularityItem>> Casted()
        {
            var result = await FetchPopularity();
            return result.Casted<TPopularityItem>();
        }
    }
}