using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public class BankierStockPopularityService : AbstractStockPopularityService<BankierStockPopularityItem>,
        IBankierPopularityService
    {
        public BankierStockPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                              ILogger<BankierStockPopularityService> logger)
            : base(httpClient, dateProvider, logger)
        {
        }


        protected override string Uri => "https://www.bankier.pl/gielda/notowania/ranking-popularnosci";
        protected override string TableXpath => "//table";
        protected override string TableRowsXpath => "//tr";
        protected override string WebsiteDisplayName => "Bankier.pl";


        protected override BankierStockPopularityItem PopularityItemFrom(string rowString)
        {
            var stringElements = rowString.Split(" ").Where(x => x != "").ToArray();

            var rank = int.Parse(stringElements.First());
            var stockName = new StockName(stringElements[1], stringElements[2]);
            var postsFromLast30DaysCount = int.Parse(stringElements[3]);
            return new BankierStockPopularityItem(stockName, rank, postsFromLast30DaysCount);
        }
    }
}