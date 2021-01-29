using AzureFunctions.Model;
using Core.Model;

namespace AzureFunctions.Services
{
    public interface IPopularityEntityFactory
    {
        PopularityEntity<Ranking> CreateEntities(Popularity<IPopularityItem> popularity);
    }
}