using System;
using System.Diagnostics.CodeAnalysis;

namespace StockPopularityFunction.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class StockPopularityEntity<TRanking> where TRanking : Ranking
    {
        public string dateTime { get; }
        private DateTimeOffset _dateTimeOffset { get; }
        public Source<TRanking> source { get; }


        public StockPopularityEntity(DateTimeOffset dateTimeOffset, Source<TRanking> source)
        {
            _dateTimeOffset = dateTimeOffset;
            dateTime = _dateTimeOffset.ToString("MM-yyyy");
            this.source = source;
        }
    }
}