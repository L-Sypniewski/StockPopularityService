using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                                                 IHtmlDocumentReader htmlDocumentReader,
                                                 ILogger<BiznesradarStockPopularityService>? logger = null)
            : base(httpClient, dateProvider, htmlDocumentReader, logger)
        {
        }


        protected override string Uri => "https://www.biznesradar.pl/symbols-rank/";
        protected override string TableXpath => "//table[@class='qTableFull symbol-rank']";
        protected override string TableRowsXpath => "tr";
        protected override string WebsiteDisplayName => "Biznesradar";


        protected override StockPopularityItem PopularityItemFrom(string rowString)
        {
            const string tempSeparator = "\t";
            var rowStringWithoutMultipleSpaces = Regex.Replace(rowString, @"\s{2,}", tempSeparator);
            var stringElements = rowStringWithoutMultipleSpaces.Split(tempSeparator)
                                                               .Where(x => !string.IsNullOrEmpty(x))
                                                               .ToArray();

            var rank = int.Parse(stringElements.First());
            var stockName = StockNameFrom(stringElements);

            return new StockPopularityItem(stockName, rank);
        }


        private static StockName StockNameFrom(IReadOnlyList<string> rowStringElements)
        {
            const int indexOfElementWithNames = 1;
            var names = rowStringElements[indexOfElementWithNames];

            var stockNameContainsTwoCodeNames = names.EndsWith(")");
            if (stockNameContainsTwoCodeNames)
            {
                var splitString = names.Split("(");
                var codename = splitString.First();
                var codenameIsIndexName = codename.StartsWith('^') || codename.Contains('.');

                var longName = codenameIsIndexName ? null : splitString.Last().TrimEnd(')');
                return new StockName(codename.TrimStart('^'), longName);
            }

            var stockIsCurrencyPair = names.CharOccurrences('/') == 2;
            if (stockIsCurrencyPair)
            {
                var codename = names.Substring(0, 7);
                var longName = names.Substring(7);
                return new StockName(codename, longName);
            }

            var stockIsCommodity = names.CharOccurrences('-') == 1;
            if (stockIsCommodity)
            {
                var splitNames = names.Split('-');
                var longName = splitNames.First();
                var codename = string.Join(' ', splitNames.Skip(1));
                return new StockName(codename, longName);
            }

            return new StockName(names);
        }


        public async Task<StockPopularity<StockPopularityItem>> FetchBiznesradarStockPopularity() => await Casted();
    }
}