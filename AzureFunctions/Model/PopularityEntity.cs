using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AzureFunctions.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public sealed class PopularityEntity<TRanking> where TRanking : Ranking
    {
        public string MonthCreated { get; }
        public string dateTime { get; }
        private DateTimeOffset _dateTimeOffset { get; }
        public Source<TRanking> source { get; }


        public PopularityEntity(DateTimeOffset dateTimeOffset, Source<TRanking> source)
        {
            _dateTimeOffset = dateTimeOffset;
            MonthCreated = _dateTimeOffset.ToString("MM-yyyy");
            dateTime = _dateTimeOffset.ToString(new CultureInfo("en-gb"));
            this.source = source;
        }
    }
}