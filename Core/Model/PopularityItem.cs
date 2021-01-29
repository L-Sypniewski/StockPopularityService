namespace Core.Model
{
    public readonly struct PopularityItem : IPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }


        public PopularityItem(StockName stockName, int rank)
        {
            StockName = stockName;
            Rank = rank;
        }


        public override string ToString() => $"StockName: {StockName}, Rank: {Rank}";
    }
}