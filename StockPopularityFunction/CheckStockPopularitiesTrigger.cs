using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.StocksPopularityService;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private readonly IAggregateStockPopularityService _aggregateStockPopularityService;


        public CheckStockPopularitiesTrigger(IAggregateStockPopularityService aggregateStockPopularityService)
        {
            _aggregateStockPopularityService = aggregateStockPopularityService;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer,
                                   ILogger log)
        {
            await foreach (var s in _aggregateStockPopularityService.FetchStockPopularityRankings()) log.LogError(s.ToString());


            // var taskAddBankierItems = bankierItems.AddAsync(bankierStockPopularity);
            // var taskAddBiznesradarItems = biznesradarItems.AddAsync(biznesRadarstocksPopularity);
            // await Task.WhenAll(/*taskAddBankierItems, */taskAddBiznesradarItems);
        }
    }
}