using System.Diagnostics.CodeAnalysis;
using Serilog.Events;

#pragma warning disable 8618
namespace AzureFunctions
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class AzureLogAnalyticsOptions
    {
        public const string ConfigName = "AzureLogAnalyticsOptions";

        public string WorkspaceId { get; set; }
        public string AuthenticationId { get; set; }
        public string LogName { get; set; }
        public LogEventLevel Level { get; set; } = LogEventLevel.Debug;
        public bool StoreTimeStampInUtc { get; set; } = true;
        public int BatchSize { get; set; } = 100;
        public int BufferSize { get; set; } = 2000;
        public bool FlattenObject { get; set; } = true;
    }
}