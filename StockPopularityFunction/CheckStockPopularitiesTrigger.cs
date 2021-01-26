using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityFunction.Services;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private readonly IAggregateStockPopularityService _aggregateStockPopularityService;
        private readonly IStockPopularityEntityFactory _stockPopularityEntityFactory;


        public CheckStockPopularitiesTrigger(IAggregateStockPopularityService aggregateStockPopularityService,
                                             IStockPopularityEntityFactory stockPopularityEntityFactory)
        {
            _aggregateStockPopularityService = aggregateStockPopularityService;
            this._stockPopularityEntityFactory = stockPopularityEntityFactory;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer,
                                   /*[CosmosDB(
                                       databaseName: "ToDoItems",
                                       collectionName: "Items",
                                       ConnectionStringSetting = "CosmosDBConnection")]
                                   IAsyncCollector<Startup> toDoItemsOut,*/
                                   ILogger log)
        {
            var testy = await _aggregateStockPopularityService.FetchStockPopularityRankings()
                                                              .Select(s => _stockPopularityEntityFactory.CreateEntities(s))
                                                              .ToListAsync();

            int i = 2;


            // var taskAddBankierItems = bankierItems.AddAsync(bankierStockPopularity);
            // var taskAddBiznesradarItems = biznesradarItems.AddAsync(biznesRadarstocksPopularity);
            // await Task.WhenAll(/*taskAddBankierItems, */taskAddBiznesradarItems);
        }
    }
}