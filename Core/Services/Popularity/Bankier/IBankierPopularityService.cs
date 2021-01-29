using System.Threading.Tasks;
using Core.Model;

namespace Core.Services.Popularity
{
    public interface IBankierPopularityService
    {
        public Task<Popularity<BankierPopularityItem>> FetchBankierPopularity();
    }
}