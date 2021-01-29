using AzureFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Services.Popularity;
using Core.Utils;
using AzureFunctions;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunctions
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


            builder.Services.AddHttpClient<IPopularityService, BiznesradarPopularityService>();
            builder.Services.AddHttpClient<IPopularityService, BankierPopularityService>();

            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();
            builder.Services.AddSingleton<IDateProvider, DateProvider>();
            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();

            builder.Services.AddSingleton<IAggregatePopularityService, AggregatePopularityService>();
            builder.Services.AddSingleton<IPopularityEntityFactory, PopularityEntityFactory>();
            builder.Services.AddSingleton<ISourceFactory, SourceFactory>();
            builder.Services.AddSingleton<IBiznesradarPopularityStockNameFactory, BiznesradarPopularityStockNameFactory>();
        }
    }
}