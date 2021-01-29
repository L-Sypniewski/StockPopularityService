using System.Threading.Tasks;
using Core.Model;

namespace Core.Services.Popularity
{
    public interface IPopularityService
    {
        public Task<Popularity<IPopularityItem>> FetchPopularity();
    }
}