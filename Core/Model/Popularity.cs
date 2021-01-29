using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public readonly struct Popularity<TPopularityItem> where TPopularityItem : IPopularityItem
    {
        public IReadOnlyCollection<TPopularityItem> Items { get; }
        public DateTimeOffset DateTime { get; }


        public Popularity(IEnumerable<TPopularityItem> items, DateTimeOffset dateTime)
        {
            Items = items.ToArray();
            DateTime = dateTime;
        }


        public Popularity<T> Casted<T>() where T : IPopularityItem
        {
            return new Popularity<T>(Items.Cast<T>(), DateTime);
        }
    }
}