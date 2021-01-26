using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public class AggregateStockPopularityService : IAggregateStockPopularityService
    {
        private readonly IEnumerable<IStockPopularityService> _stockPopularityServices;
        private readonly ILogger<AggregateStockPopularityService> _logger;


        public AggregateStockPopularityService(IEnumerable<IStockPopularityService> stockPopularityServices,
                                               ILogger<AggregateStockPopularityService>? logger)
        {
            _stockPopularityServices = stockPopularityServices;
            _logger = logger ?? NullLogger<AggregateStockPopularityService>.Instance;
        }


        public IAsyncEnumerable<StockPopularity<IStockPopularityItem>> FetchStockPopularityRankings()
        {
            _logger.LogInformation("Fetching data from services executed at: {Time} (UTC)", DateTime.UtcNow);

            var tasks = _stockPopularityServices.Select(service => service.FetchStockPopularity());
            return tasks.ParallelEnumerateAsync();
        }
    }
}