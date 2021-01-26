using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace StockPopularityCore.Utils
{
    public interface IHtmlDocumentReader // TODO: Remove and use nuget with my custom extensions
    {
        HtmlDocument HtmlDocumentFrom(string htmlString);
    }

    public class HtmlDocumentReader : IHtmlDocumentReader
    {
        private readonly ILogger<HtmlDocumentReader> _logger;


        public HtmlDocumentReader(ILogger<HtmlDocumentReader>? logger = null)
        {
            _logger = logger ?? NullLogger<HtmlDocumentReader>.Instance;
        }


        public HtmlDocument HtmlDocumentFrom(string htmlString)
        {
            _logger.LogDebug("Creating HTML document from string");
            _logger.LogTrace("Html string: {HtmlString}", htmlString);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlString);
            return htmlDoc;
        }
    }
}