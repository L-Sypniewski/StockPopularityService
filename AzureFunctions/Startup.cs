using System;
using System.IO;
using AzureFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Services.Popularity;
using Core.Utils;
using AzureFunctions;
using Core.Utils.Logging;
using Serilog;
using Serilog.Events;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {


        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterServices(builder);

            var environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");

            builder
                .Services
                .AddOptions<CosmosDbOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration
                        .GetSection(CosmosDbOptions.ConfigName)
                        .Bind(options);
                });

            builder
                .Services
                .AddOptions<AzureLogAnalyticsOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration
                        .GetSection(AzureLogAnalyticsOptions.ConfigName)
                        .Bind(options);
                });

            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithCorrelationId()
                         .Enrich.FromLogContext()
                         .Enrich.WithProperty("Application", "Stock Popularity")
                         .Enrich.WithProperty("Environment", environment)
                         .WriteTo.Console(
                             outputTemplate:
                             "[{Timestamp:HH:mm:ss} {CorrelationId} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                         .WriteTo.AzureAnalytics("11f03e2b-a198-424f-a4f4-ffda56672e9d",
                                                 "/2zMJccaOJUsbbTNHi5mCD/vE9q6Qt9KSKLlThBKxAr4P4LVItOEkp0/zsocd974MvqOU0UxpaG5hf4QlacYuA==",
                                                 "Stock data toolset", LogEventLevel.Debug, true, batchSize: 1)
                         .CreateLogger();
            builder.Services.AddLogging(c => c.AddSerilog(Log.Logger));
        }


        private static void RegisterServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient<IPopularityService, BiznesradarPopularityService>();
            builder.Services.AddHttpClient<IPopularityService, BankierPopularityService>();

            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();
            builder.Services.AddSingleton<IDateProvider, DateProvider>();
            builder.Services.AddSingleton<IHtmlDocumentReader, HtmlDocumentReader>();

            builder.Services.AddSingleton<IAggregatePopularityService, AggregatePopularityService>();
            builder.Services.AddSingleton<IPopularityEntityFactory, PopularityEntityFactory>();
            builder.Services.AddSingleton<ISourceFactory, SourceFactory>();
            builder.Services.AddSingleton<IBiznesradarPopularityStockNameFactory, BiznesradarPopularityStockNameFactory>();
            builder.Services.AddSingleton<IPopularityItemTypeFactory, PopularityItemTypeFactory>();
            builder.Services.AddTransient<ICorrelationIdProvider, CorrelationIdProvider>();
        }


        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();
            var ctxName = context.EnvironmentName;

            builder.ConfigurationBuilder
                   .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                   .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                   .AddEnvironmentVariables();
        }
    }
}