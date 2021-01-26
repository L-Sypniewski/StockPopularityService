// ReSharper disable All
namespace StockPopularityFunction.Model
{
    public class Source
    {
        public string name { get; }
        public Ranking[] rankings { get; }


        public Source(string name, Ranking[] rankings)
        {
            this.name = name;
            this.rankings = rankings;
        }
    }
}