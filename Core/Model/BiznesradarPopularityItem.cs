namespace Core.Model
{
    public readonly struct BiznesradarPopularityItem : IPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }


        public BiznesradarPopularityItem(StockName stockName, int rank)
        {
            StockName = stockName;
            Rank = rank;
        }


        public override string ToString() => $"StockName: {StockName}, Rank: {Rank}";
    }
}