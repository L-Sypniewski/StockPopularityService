using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.StocksPopularityService;

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
            var biznesRadarstocksPopularityTask = _biznesradarPopularityService.FetchStockPopularity();
            var bankierStockPopularityTask = _bankierPopularityService.FetchStockPopularity();
            await Task.WhenAll(biznesRadarstocksPopularityTask, bankierStockPopularityTask);

            var biznesRadarstocksPopularity = biznesRadarstocksPopularityTask.Result;
            var bankierStockPopularity = bankierStockPopularityTask.Result;
            int i = 2;
        }
    }
}