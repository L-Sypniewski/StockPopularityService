using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Model;
using StockPopularityCore.Utils;

namespace StockPopularityCore.Services.PopularityService
{
    public class BiznesradarPopularityService : AbstractPopularityService<StockPopularityItem>, IBiznesradarPopularityService
    {
        public BiznesradarPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                            ILogger<BiznesradarPopularityService> logger)
            : base(httpClient, dateProvider, logger)
        {
        }


        protected override string Uri => "https://www.biznesradar.pl/symbols-rank/";
        protected override string TableXpath => "//table[@class='qTableFull symbol-rank']";
        protected override string TableRowsXpath => "tr";
        protected override string WebsiteDisplayName => "Biznesradar";


        protected override StockPopularityItem PopularityItemFrom(string rowString)
        {
            var stringElements = rowString.Split(" ").Where(x => x != "").ToArray();

            var rank = int.Parse(stringElements.First());
            var stockName = StockNameFrom(stringElements);

            return new StockPopularityItem(stockName, rank);
        }


        private static StockName StockNameFrom(string[] rowStringElements)
        {
            var stockNameContainsTwoCodeNames = rowStringElements.Length == 11;
            var codename = rowStringElements[1];
            var longName = stockNameContainsTwoCodeNames
                ? rowStringElements[2].WithoutFirstAndLastCharacter()
                : null;
            return new StockName(longName, codename);
        }
    }
}