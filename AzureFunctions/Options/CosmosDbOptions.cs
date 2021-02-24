using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace AzureFunctions.Options
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class CosmosDbOptions
    {
        public const string ConfigName = "CosmosDbOptions";

        public string DatabaseName { get; private set; }
        public string CollectionName  { get; private set; }
        public string Endpoint  { get; private set; }
        public string Key  { get; private set; }
    }
}