using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StockPopularityCore.Services.StocksPopularityService;
using StockPopularityCore.Utils;
using StockPopularityFunction;

[assembly: FunctionsStartup(typeof(Startup))]

namespace StockPopularityFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IStockPopularityService, BiznesradarStockPopularityService>();
            builder.Services.AddHttpClient<IStockPopularityService, BankierPopularityService>();

            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();
            builder.Services.AddSingleton<IDateProvider, DateProvider>();

            builder.Services.AddSingleton<IAggregateStockPopularityService, AggregateStockPopularityService>();
        }


        public static bool IsDevelopmentEnvironment =>
            Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development";
    }
}