using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityFunction.Services;

namespace StockPopularityFunction
{
    public class CheckStockPopularitiesTrigger
    {
        private static readonly Lazy<DocumentClient> _lazyClient =
            new Lazy<DocumentClient>(InitializeDocumentClient);

        private static DocumentClient DocumentClient => _lazyClient.Value;

        private static StockPopularityDbOptions? CosmosDbOptions { get; set; }

        private readonly IAggregateStockPopularityService _aggregateStockPopularityService;
        private readonly IStockPopularityEntityFactory _stockPopularityEntityFactory;


        private static DocumentClient InitializeDocumentClient()
        {
            var options = CosmosDbOptions!;
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return new DocumentClient(new Uri(options.Endpoint), options.Key, settings);
        }


        public CheckStockPopularitiesTrigger(IAggregateStockPopularityService aggregateStockPopularityService,
                                             IStockPopularityEntityFactory stockPopularityEntityFactory,
                                             IOptions<StockPopularityDbOptions> options)
        {
            CosmosDbOptions = options.Value;
            _aggregateStockPopularityService = aggregateStockPopularityService;
            _stockPopularityEntityFactory = stockPopularityEntityFactory;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("%TimerExpression%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("CheckStockPopularitiesTrigger start");

            log.LogDebug("Setting static StockPopularityDbOptions");


            Uri collectionUri =
                UriFactory.CreateDocumentCollectionUri(CosmosDbOptions!.DatabaseName, CosmosDbOptions.CollectionName);

            var stockPopularityEntities = _aggregateStockPopularityService.FetchStockPopularityRankings()
                                                                          .Select(item => _stockPopularityEntityFactory
                                                                                      .CreateEntities(item));
            await foreach (var entity in stockPopularityEntities)
            {
                await DocumentClient.UpsertDocumentAsync(collectionUri, entity);
            }
        }
    }
}