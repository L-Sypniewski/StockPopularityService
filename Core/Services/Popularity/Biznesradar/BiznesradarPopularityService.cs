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
        private readonly IBiznesradarPopularityStockNameFactory _stockNameFactory;


        public BiznesradarPopularityService(HttpClient httpClient, IDateProvider dateProvider,
                                            IHtmlDocumentReader htmlDocumentReader,
                                            IBiznesradarPopularityStockNameFactory stockNameFactory,
                                            ILogger<BiznesradarPopularityService>? logger = null)
            : base(httpClient, dateProvider, htmlDocumentReader, logger)
        {
            _stockNameFactory = stockNameFactory;
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
            var stockName = _stockNameFactory.CreateFrom(nameElement);


            var rank = int.Parse(stringElements.First());
            return new BiznesradarPopularityItem(stockName, rank);
        }


        private static string NameFromStringElements(string[] stringElements)
        {
            const int indexOfElementWithName = 1;
            var name = stringElements[indexOfElementWithName];
            return name;
        }

        public async Task<Popularity<BiznesradarPopularityItem>> FetchBiznesradarPopularity() => await Casted();
    }
}