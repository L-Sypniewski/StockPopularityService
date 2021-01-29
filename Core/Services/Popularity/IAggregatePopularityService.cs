using System.Collections.Generic;
using Core.Model;

namespace Core.Services.Popularity
{
    public interface IAggregatePopularityService
    {
        IAsyncEnumerable<Popularity<IPopularityItem>> FetchPopularityRankings();
    }
}