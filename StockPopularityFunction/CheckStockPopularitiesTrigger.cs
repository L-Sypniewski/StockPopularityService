using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.Biznesradar;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private readonly IBiznesradarPopularityService _biznesradarPopularityService;


        public CheckStockPopularitiesTrigger(IBiznesradarPopularityService biznesradarPopularityService)
        {
            _biznesradarPopularityService = biznesradarPopularityService;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"CheckStockPopularitiesTrigger executed at: {DateTime.UtcNow}");
            var stocksPopularity = await _biznesradarPopularityService.FetchStocksPopularity();
            int i = 2;
        }
    }
}