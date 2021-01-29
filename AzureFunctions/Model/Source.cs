// ReSharper disable All

using System.Collections.Generic;
using System.Linq;

namespace AzureFunctions.Model
{
    public sealed class Source<TRanking> where TRanking : Ranking

    {
        public string name { get; }
        public TRanking[] rankings { get; }


        public Source(string name, IEnumerable<TRanking> rankings)
        {
            this.name = name;
            this.rankings = rankings.ToArray();
        }
    }
}