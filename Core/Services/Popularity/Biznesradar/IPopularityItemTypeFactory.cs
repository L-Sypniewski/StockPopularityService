using Core.Model;

namespace Core.Services.Popularity
{
    public interface IPopularityItemTypeFactory
    {
        PopularityItemType? CreateTypeFrom(string name);
    }
}