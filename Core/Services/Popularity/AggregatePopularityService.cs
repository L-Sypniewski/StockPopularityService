using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Core.Utils;

namespace Core.Services.Popularity
{
    public class AggregatePopularityService : IAggregatePopularityService
    {
        private readonly IEnumerable<IPopularityService> _popularityServices;
        private readonly ILogger<AggregatePopularityService> _logger;


        public AggregatePopularityService(IEnumerable<IPopularityService> popularityServices,
                                               ILogger<AggregatePopularityService>? logger)
        {
            _popularityServices = popularityServices;
            _logger = logger ?? NullLogger<AggregatePopularityService>.Instance;
        }


        public IAsyncEnumerable<Popularity<IPopularityItem>> FetchPopularityRankings()
        {
            _logger.LogInformation("Fetching data from services executed at: {Time} (UTC)", DateTime.UtcNow);

            var tasks = _popularityServices.Select(service => service.FetchPopularity());
            return tasks.ParallelEnumerateAsync();
        }
    }
}