using System;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StockPopularityCore.Services.Biznesradar;
using StockPopularityCore.Utils;

[assembly: FunctionsStartup(typeof(StockPopularityFunction.Startup))]

namespace StockPopularityFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IBiznesradarPopularityService, BiznesradarPopularityService>();
            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();
            builder.Services.AddSingleton<IDateProvider, DateProvider>();
        }


        public static bool IsDevelopmentEnvironment =>
            Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development";
    }
}