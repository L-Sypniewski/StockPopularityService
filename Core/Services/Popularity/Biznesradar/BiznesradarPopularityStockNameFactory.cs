using System;
using System.Linq;
using Core.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Core.Services.Popularity
{
    public class BiznesradarPopularityStockNameFactory : IBiznesradarPopularityStockNameFactory
    {
        private readonly IPopularityItemTypeFactory _popularityItemTypeFactory;
        private readonly ILogger<BiznesradarPopularityStockNameFactory> _logger;


        public BiznesradarPopularityStockNameFactory(IPopularityItemTypeFactory popularityItemTypeFactory, ILogger<BiznesradarPopularityStockNameFactory>? logger = null)
        {
            _popularityItemTypeFactory = popularityItemTypeFactory;
            _logger = logger ?? NullLogger<BiznesradarPopularityStockNameFactory>.Instance;
        }


        public StockName CreateFrom(string name)
        {
            try
            {
                _logger.LogInformation("Creating StockName for: {Name}", name);

                var type = _popularityItemTypeFactory.CreateTypeFrom(name);
                _logger.LogInformation("Created {Type} item type for {Name}", type.ToString(), name);

                Func<string, ILogger, StockName> createNameFunc = type switch
                {
                    PopularityItemType.Commodity => CreateForCommodity,
                    PopularityItemType.Index => CreateForIndex,
                    PopularityItemType.Stock => CreateForStock,
                    PopularityItemType.Currency => CreateForCurrency,
                    null => CreateForUnknownType,
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };

                return createNameFunc(name, _logger);
            }
            catch (Exception exception)
            {
                _logger.LogError("Failed to create StockName for Biznesradar, {Exception}", exception);
                throw;
            }
        }
        private static StockName CreateForCurrency(string name, ILogger logger)
        {
            logger.LogDebug("CreateForCurrency: {Name}", name);

            var codename = name.Substring(0, 7);
            var longName = name.Substring(7);
            return new StockName(codename, longName);
        }


        private static StockName CreateForCommodity(string name, ILogger logger)
        {
            logger.LogDebug("CreateForCommodity: {Name}", name);

            var splitNames = name.Split('-');
            var longName = splitNames.First();
            var codename = string.Join(' ', splitNames.Skip(1));
            return new StockName(codename, longName);
        }


        private static StockName CreateForStock(string name, ILogger logger)
        {
            logger.LogDebug("CreateForStock: {Name}", name);

            var splitString = name.Split("(");
            var codename = splitString.First();
            var longName = splitString.Last().TrimEnd(')');
            return new StockName(codename.TrimStart('^'), longName);
        }


        private static StockName CreateForIndex(string name, ILogger logger)
        {
            logger.LogDebug("CreateForIndex: {Name}", name);

            var splitString = name.Split("(");
            var codename = splitString.First();
            return new StockName(codename.TrimStart('^'));
        }


        private static StockName CreateForUnknownType(string name, ILogger logger)
        {
            logger.LogDebug("CreateForUnknownType: {Name}", name);

            return new StockName(name);
        }
    }
}