using System.IO;
using AzureFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Services.Popularity;
using Core.Utils;
using AzureFunctions;
using AzureFunctions.Options;
using Core.Utils.Logging;
using Serilog;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterServices(builder);

            var builderContext = builder.GetContext()!;
            var environment = builderContext.EnvironmentName;

            builder
                .Services
                .AddOptions<CosmosDbOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration
                        .GetSection(CosmosDbOptions.ConfigName)
                        .Bind(options, x => x.BindNonPublicProperties = true);
                });

            var azureLogAnalyticsOptions = new AzureLogAnalyticsOptions();
            builderContext.Configuration.GetSection(AzureLogAnalyticsOptions.ConfigName)
                          .Bind(azureLogAnalyticsOptions, x => x.BindNonPublicProperties = true);

            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithCorrelationId()
                         .Enrich.FromLogContext()
                         .Enrich.WithProperty("Application", "Stock Popularity")
                         .Enrich.WithProperty("Environment", environment)
                         .WriteTo.Console(
                             outputTemplate:
                             "[{Timestamp:HH:mm:ss} {CorrelationId} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                         .WriteTo.AzureAnalytics(azureLogAnalyticsOptions.WorkspaceId,
                                                 azureLogAnalyticsOptions.AuthenticationId,
                                                 azureLogAnalyticsOptions.LogName, azureLogAnalyticsOptions.Level,
                                                 azureLogAnalyticsOptions.StoreTimeStampInUtc,
                                                 batchSize: azureLogAnalyticsOptions.BatchSize)
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
            var environmentName = context.EnvironmentName;

            builder.ConfigurationBuilder
                   .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true,
                                reloadOnChange: false)
                   .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{environmentName}.json"),
                                optional: true, reloadOnChange: false)
                   .AddEnvironmentVariables();
        }
    }
}