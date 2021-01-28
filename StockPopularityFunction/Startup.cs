using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityCore.Utils;
using StockPopularityFunction;
using StockPopularityFunction.Services;

[assembly: FunctionsStartup(typeof(Startup))]

namespace StockPopularityFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder
                .Services
                .AddOptions<StockPopularityDbOptions>()
                .Configure<IConfiguration>((messageResponderSettings, configuration) =>
                {
                    configuration
                        .GetSection(StockPopularityDbOptions.ConfigName)
                        .Bind(messageResponderSettings);
                });


            builder.Services.AddHttpClient<IStockPopularityService, BiznesradarStockPopularityService>();
            builder.Services.AddHttpClient<IStockPopularityService, BankierPopularityService>();

            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();
            builder.Services.AddSingleton<IDateProvider, DateProvider>();
            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();

            builder.Services.AddSingleton<IAggregateStockPopularityService, AggregateStockPopularityService>();
            builder.Services.AddSingleton<IStockPopularityEntityFactory, StockPopularityEntityFactory>();
            builder.Services.AddSingleton<ISourceFactory, SourceFactory>();
        }
    }
}