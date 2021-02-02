using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace AzureFunctions
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class CosmosDbOptions
    {
        public const string ConfigName = "CosmosDbOptions";

        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string Endpoint { get; set; }
        public string Key { get; set; }
    }
}