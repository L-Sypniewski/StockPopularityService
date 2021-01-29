using System;
using System.Linq;
using System.Threading.Tasks;
using AzureFunctions.Services;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Core.Services.Popularity;

namespace AzureFunctions
{
    public class CheckStockPopularitiesTrigger
    {
        private static readonly Lazy<DocumentClient> _lazyClient =
            new Lazy<DocumentClient>(InitializeDocumentClient);

        private static DocumentClient DocumentClient => _lazyClient.Value;

        private static StockPopularityDbOptions? CosmosDbOptions { get; set; }

        private readonly IAggregatePopularityService _aggregatePopularityService;
        private readonly IPopularityEntityFactory _popularityEntityFactory;


        private static DocumentClient InitializeDocumentClient()
        {
            var options = CosmosDbOptions!;
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return new DocumentClient(new Uri(options.Endpoint), options.Key, settings);
        }


        public CheckStockPopularitiesTrigger(IAggregatePopularityService aggregatePopularityService,
                                             IPopularityEntityFactory popularityEntityFactory,
                                             IOptions<StockPopularityDbOptions> options)
        {
            CosmosDbOptions = options.Value;
            _aggregatePopularityService = aggregatePopularityService;
            _popularityEntityFactory = popularityEntityFactory;
        }


        [FunctionName("CheckStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("%TimerExpression%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("CheckStockPopularitiesTrigger start");

            log.LogDebug("Setting static StockPopularityDbOptions");


            Uri collectionUri =
                UriFactory.CreateDocumentCollectionUri(CosmosDbOptions!.DatabaseName, CosmosDbOptions.CollectionName);

            var popularityEntities = _aggregatePopularityService.FetchPopularityRankings()
                                                                          .Select(item => _popularityEntityFactory
                                                                                      .CreateEntities(item));
            await foreach (var entity in popularityEntities)
            {
                await DocumentClient.UpsertDocumentAsync(collectionUri, entity);
            }
        }
    }
}