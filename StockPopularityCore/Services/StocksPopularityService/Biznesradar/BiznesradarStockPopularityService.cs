using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.StocksPopularityService
{
    public class BiznesradarStockPopularityService : AbstractStockPopularityService<StockPopularityItem>,
        IBiznesradarPopularityService
    {
        public BiznesradarStockPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                                 ILogger<BiznesradarStockPopularityService> logger)
            : base(httpClient, dateProvider, logger)
        {
        }


        protected override string Uri => "https://www.biznesradar.pl/symbols-rank/";
        protected override string TableXpath => "//table[@class='qTableFull symbol-rank']";
        protected override string TableRowsXpath => "tr";
        protected override string WebsiteDisplayName => "Biznesradar";


        protected override StockPopularityItem PopularityItemFrom(string rowString)
        {
            var stringElements = rowString.Split(" ")
                                          .Where(x => !string.IsNullOrEmpty(x))
                                          .ToArray();

            var rank = int.Parse(stringElements.First());
            var stockName = StockNameFrom(stringElements);

            return new StockPopularityItem(stockName, rank);
        }


        private static StockName StockNameFrom(IReadOnlyList<string> rowStringElements)
        {
            var stockNameContainsTwoCodeNames = rowStringElements.Count == 11;
            var codename = rowStringElements[1];
            var longName = stockNameContainsTwoCodeNames
                ? rowStringElements[2].WithoutFirstAndLastCharacter()
                : null;
            return new StockName(longName, codename);
        }


        public async Task<StockPopularity<StockPopularityItem>> FetchBiznesradarStockPopularity() => await Casted();
    }
}