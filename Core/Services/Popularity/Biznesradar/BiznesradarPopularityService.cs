using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Model;
using Core.Utils;
using Microsoft.Extensions.Logging;

namespace Core.Services.Popularity
{
    public class BiznesradarPopularityService : AbstractPopularityService<BiznesradarPopularityItem>,
        IBiznesradarPopularityService
    {
        public BiznesradarPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                            IHtmlDocumentReader htmlDocumentReader,
                                            ILogger<BiznesradarPopularityService>? logger = null)
            : base(httpClient, dateProvider, htmlDocumentReader, logger)
        {
        }


        protected override string Uri => "https://www.biznesradar.pl/symbols-rank/";
        protected override string TableXpath => "//table[@class='qTableFull symbol-rank']";
        protected override string TableRowsXpath => "tr";
        protected override string WebsiteDisplayName => "Biznesradar";


        protected override BiznesradarPopularityItem PopularityItemFrom(string rowString)
        {
            const string tempSeparator = "\t";
            var rowStringWithoutMultipleSpaces = Regex.Replace(rowString, @"\s{2,}", tempSeparator);
            var stringElements = rowStringWithoutMultipleSpaces.Split(tempSeparator)
                                                               .Where(row => !string.IsNullOrEmpty(row))
                                                               .ToArray();

            var nameElement = NameFromStringElements(stringElements);
            var itemType = TypeFrom(nameElement);
            var stockName = StockNameFrom(nameElement, itemType);


            var rank = int.Parse(stringElements.First());
            return new BiznesradarPopularityItem(stockName, rank);
        }


        private static string NameFromStringElements(string[] stringElements)
        {
            const int indexOfElementWithName = 1;
            var name = stringElements[indexOfElementWithName];
            return name;
        }


        private static PopularityItemType? TypeFrom(string names)
        {
            var stockNameContainsTwoCodeNames = names.EndsWith(")");
            if (stockNameContainsTwoCodeNames)
            {
                var splitString = names.Split("(");
                var codename = splitString.First();
                var codenameIsIndexName = codename.StartsWith('^') || codename.Contains('.');

                return codenameIsIndexName ? PopularityItemType.Index : PopularityItemType.Stock;
            }

            var stockIsCurrencyPair = names.CharOccurrences('/') == 2;
            if (stockIsCurrencyPair)
            {
                return PopularityItemType.Currency;
            }

            var stockIsCommodity = names.CharOccurrences('-') == 1;
            if (stockIsCommodity)
            {
                return PopularityItemType.Commodity;
            }

            return null;
        }


        private static StockName StockNameFrom(string names, PopularityItemType? type)
        {

            if (type == PopularityItemType.Currency)
            {
                var codename = names.Substring(0, 7);
                var longName = names.Substring(7);
                return new StockName(codename, longName);
            }

            if (type == PopularityItemType.Commodity)
            {
                var splitNames = names.Split('-');
                var longName = splitNames.First();
                var codename = string.Join(' ', splitNames.Skip(1));
                return new StockName(codename, longName);
            }


            var splitStringX = names.Split("(");
            var codenameX = splitStringX.First();
            if (type == PopularityItemType.Index)
            {

                return new StockName(codenameX.TrimStart('^'));
            }

            if (type == PopularityItemType.Stock)
            {
                var longName = splitStringX.Last().TrimEnd(')');
                return new StockName(codenameX.TrimStart('^'),longName);
            }

            return new StockName(names);
        }


        public async Task<Popularity<BiznesradarPopularityItem>> FetchBiznesradarPopularity() => await Casted();
    }
}