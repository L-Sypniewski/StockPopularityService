using System;
using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class StockPopularityEntity
    {
        public string dateTime { get; }
        public Source source { get; }

        public StockPopularityEntity(DateTimeOffset dateTime, Source source)
        {
            this.dateTime = dateTime.ToString("MM-YYYY");
            this.source = source;
        }
    }
}