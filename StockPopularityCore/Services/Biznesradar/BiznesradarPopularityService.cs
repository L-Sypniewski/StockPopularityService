using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.Biznesradar
{
    public class BiznesradarPopularityService : IBiznesradarPopularityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BiznesradarPopularityService> _logger;
        private const string Uri = "https://www.biznesradar.pl/symbols-rank/";
        private readonly IHtmlDocumentReader _documentReader;


        public BiznesradarPopularityService(HttpClient httpClient, ILogger<BiznesradarPopularityService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _documentReader = new HtmlDocumentReader();
        }


        public async IAsyncEnumerable<IStockPopularity> StocksPopularity()
        {
            var source = await _httpClient.GetStringAsync(Uri);
            _logger.LogInformation("Got site: {uri}", Uri);
            var document = _documentReader.HtmlDocumentFrom(source);


            yield return default(IStockPopularity);
        }
    }
}