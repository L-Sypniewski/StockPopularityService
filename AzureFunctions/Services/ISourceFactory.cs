using AzureFunctions.Model;
using Core.Model;

namespace AzureFunctions.Services
{
    public interface ISourceFactory
    {
        Source<Ranking> CreateFrom(Popularity<IPopularityItem> popularity);
    }
}