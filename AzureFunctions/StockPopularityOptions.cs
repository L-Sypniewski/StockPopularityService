using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace AzureFunctions
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class StockPopularityDbOptions
    {
        public const string ConfigName = "StockPopularityCosmosDbOptions";

        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string Endpoint { get; set; }
        public string Key { get; set; }
    }
}