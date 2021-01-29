using AzureFunctions.Model;
using Core.Model;

namespace AzureFunctions.Services
{
    public class PopularityEntityFactory : IPopularityEntityFactory
    {
        private readonly ISourceFactory _sourceFactory;


        public PopularityEntityFactory(ISourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public PopularityEntity<Ranking> CreateEntities(Popularity<IPopularityItem> popularity)
        {
            var source = _sourceFactory.CreateFrom(popularity);
            return new PopularityEntity<Ranking>(popularity.DateTime, source);
        }
    }
}