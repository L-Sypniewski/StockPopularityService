using System;
using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class StockPopularityEntity<TRanking> where TRanking : Ranking
    {
        public string MonthCreated { get; }
        public string dateTime { get; }
        private DateTimeOffset _dateTimeOffset { get; }
        public Source<TRanking> source { get; }


        public StockPopularityEntity(DateTimeOffset dateTimeOffset, Source<TRanking> source)
        {
            _dateTimeOffset = dateTimeOffset;
            MonthCreated = _dateTimeOffset.ToString("MM-yyyy");
            dateTime = _dateTimeOffset.ToString();
            this.source = source;
        }
    }
}