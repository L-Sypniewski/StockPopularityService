namespace Core.Model
{
    public readonly struct BiznesradarPopularityItem : IPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }
        public PopularityItemType? Type { get; }


        public BiznesradarPopularityItem(StockName stockName, int rank, PopularityItemType? type = null)
        {
            StockName = stockName;
            Rank = rank;
            Type = type;
        }


        public override string ToString() => $"StockName: {StockName}, Rank: {Rank}";
    }
}