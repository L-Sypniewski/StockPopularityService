namespace Core.Model
{
    public readonly struct BankierPopularityItem : IPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }
        public int PostsFromLast30DaysCount { get; }


        public BankierPopularityItem(StockName stockName, int rank, int postsFromLast30DaysCount)
        {
            StockName = stockName;
            Rank = rank;
            PostsFromLast30DaysCount = postsFromLast30DaysCount;
        }


        public override string ToString() =>
            $"StockName: {StockName}, Rank: {Rank}, PostsFromLast30DaysCount: {PostsFromLast30DaysCount}";
    }
}