using System;

namespace StockPopularityCore.Utils
{
    public interface IDateProvider // TODO: Use nuget internal package
    {
        DateTimeOffset Now { get; }
    }

    public class DateProvider : IDateProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}