using System.Linq;
using Core.Model;
using Core.Utils;

namespace Core.Services.Popularity
{
    public class PopularityItemTypeFactory : IPopularityItemTypeFactory
    {
        public PopularityItemType? CreateTypeFrom(string name)
        {
            var stockNameContainsTwoCodename = name.EndsWith(")");
            if (stockNameContainsTwoCodename)
            {
                var splitString = name.Split("(");
                var codename = splitString.First();
                var codenameIsIndexName = codename.StartsWith('^') || codename.Contains('.');

                return codenameIsIndexName ? PopularityItemType.Index : PopularityItemType.Stock;
            }

            var stockIsCurrencyPair = name.CharOccurrences('/') == 2;
            if (stockIsCurrencyPair)
            {
                return PopularityItemType.Currency;
            }

            var stockIsCommodity = name.CharOccurrences('-') == 1;
            if (stockIsCommodity)
            {
                return PopularityItemType.Commodity;
            }

            if (!name.IsSingleWord())
            {
                return null;
            }

            return name.Length == 3 && ( name.ToLower() != "wig" && name.ToLower() != "dax" )
                ? PopularityItemType.Stock
                : PopularityItemType.Index;
        }
    }
}