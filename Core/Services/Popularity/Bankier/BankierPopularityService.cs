using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Model;
using Core.Utils;
using Microsoft.Extensions.Logging;

namespace Core.Services.Popularity
{
    public class BankierPopularityService : AbstractPopularityService<BankierPopularityItem>,
        IBankierPopularityService
    {
        public BankierPopularityService(HttpClient httpClient, IDateProvider dateProvider, IHtmlDocumentReader htmlDocumentReader,
                                        ILogger<BankierPopularityService>? logger = null)
            : base(httpClient, dateProvider, htmlDocumentReader, logger)
        {
        }


        protected override string Uri => "https://www.bankier.pl/gielda/notowania/ranking-popularnosci";
        protected override string TableXpath => "//table";
        protected override string TableRowsXpath => "//tr";
        protected override string WebsiteDisplayName => "Bankier.pl";


        protected override BankierPopularityItem PopularityItemFrom(string rowString)
        {
            var stringElements = rowString.Split(" ").Where(x => x != "").ToArray();

            var rank = int.Parse(stringElements.First());
            var stockName = new StockName(stringElements[2], stringElements[1]);
            var postsFromLast30DaysCount = int.Parse(stringElements[3]);
            return new BankierPopularityItem(stockName, rank, postsFromLast30DaysCount);
        }


        public async Task<Popularity<BankierPopularityItem>> FetchBankierPopularity() => await Casted();
    }
}