using Core.Model;

namespace Core.Services.Popularity
{
    public interface IBiznesradarPopularityStockNameFactory
    {
        StockName CreateFrom(string name);
    }
}