using System.Diagnostics.CodeAnalysis;
using Serilog.Events;

#pragma warning disable 8618
namespace AzureFunctions.Options
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class AzureLogAnalyticsOptions
    {
        public const string ConfigName = "AzureLogAnalyticsOptions";

        public string WorkspaceId { get; private set; }
        public string AuthenticationId { get; private set; }
        public string LogName { get; private set; }
        public LogEventLevel Level { get; private set; } = LogEventLevel.Debug;
        public bool StoreTimeStampInUtc { get; private set; } = true;
        public int BatchSize { get; private set; } = 100;
    }
}