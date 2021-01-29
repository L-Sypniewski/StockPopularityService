namespace Core.Model
{
    public interface IPopularityItem
    {
        public StockName StockName { get; }
        public int Rank { get; }
    }
}