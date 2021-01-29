using System.Threading.Tasks;
using Core.Model;

namespace Core.Services.Popularity
{
    public interface IBiznesradarPopularityService
    {
        public Task<Popularity<PopularityItem>> FetchBiznesradarPopularity();
    }
}