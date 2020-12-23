using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.Biznesradar;
using StockPopularityCore.Utils;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private readonly IHtmlDocumentReader _htmlDocumentReader;
        private readonly IBiznesradarPopularityService _biznesradarPopularityService;


        public CheckStockPopularitiesTrigger(IBiznesradarPopularityService biznesradarPopularityService,
                                             IHtmlDocumentReader htmlDocumentReader)
        {
            _biznesradarPopularityService = biznesradarPopularityService;
            _htmlDocumentReader = htmlDocumentReader;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"CheckStockPopularitiesTrigger executed at: {DateTime.UtcNow}");
            var stocksPopularity = _biznesradarPopularityService.FetchStocksPopularity().Result;
            int i = 2;
        }
    }
}