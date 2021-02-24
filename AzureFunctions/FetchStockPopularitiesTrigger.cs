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
using Core.Utils.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AzureFunctions
{
    public class CheckStockPopularitiesTrigger
    {
        private static readonly Lazy<DocumentClient> _lazyClient =
            new Lazy<DocumentClient>(InitializeDocumentClient);

        private static DocumentClient DocumentClient => _lazyClient.Value;

        private static CosmosDbOptions? CosmosDbOptions { get; set; }

        private readonly IAggregatePopularityService _aggregatePopularityService;
        private readonly IPopularityEntityFactory _popularityEntityFactory;
        private readonly ICorrelationIdProvider _correlationIdProvider;


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
                                             ICorrelationIdProvider correlationIdProvider,
                                             IOptions<CosmosDbOptions> options)
        {
            CosmosDbOptions = options.Value;
            _aggregatePopularityService = aggregatePopularityService;
            _popularityEntityFactory = popularityEntityFactory;
            _correlationIdProvider = correlationIdProvider;
        }


        [FunctionName("FetchStockPopularitiesTrigger")]
        public async Task RunAsync([TimerTrigger("%TimerExpression%")] TimerInfo myTimer, ILogger log)
        {
          log.LogInformation("FetchStockPopularitiesTrigger start");


            Uri collectionUri =
                UriFactory.CreateDocumentCollectionUri(CosmosDbOptions!.DatabaseName, CosmosDbOptions.CollectionName);
            log.LogInformation("Created document collection Uri");

            var popularityEntities = _aggregatePopularityService.FetchPopularityRankings()
                                                                .Select(item => _popularityEntityFactory
                                                                            .CreateEntities(item));
            await foreach (var entity in popularityEntities)
            {
                log.LogDebug("Saving entity to database: {Entity}", entity);
                await DocumentClient.UpsertDocumentAsync(collectionUri, entity);
            }
        }
    }
}