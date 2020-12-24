using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.Bankier;
using StockPopularityCore.Services.Biznesradar;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private readonly IBiznesradarPopularityService _biznesradarPopularityService;
        private readonly IBankierPopularityService _bankierPopularityService;


        public CheckStockPopularitiesTrigger(IBiznesradarPopularityService biznesradarPopularityService,
                                             IBankierPopularityService bankierPopularityService)
        {
            _biznesradarPopularityService = biznesradarPopularityService;
            _bankierPopularityService = bankierPopularityService;
            _biznesradarPopularityService = biznesradarPopularityService;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"CheckStockPopularitiesTrigger executed at: {DateTime.UtcNow}");
            var biznesRadarstocksPopularity = await _biznesradarPopularityService.FetchStocksPopularity();
            var bankierStocksPopularity = await _bankierPopularityService.FetchStocksPopularity();
            var i = 2;
        }
    }
}