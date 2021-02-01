using System.Linq;
using Core.Model;
using Core.Utils;

namespace Core.Services.Popularity
{
    public class PopularityItemTypeFactory : IPopularityItemTypeFactory
    {
        public PopularityItemType? CreateTypeFrom(string name)
        {
            if (name.IsSingleWord())
            {
                return null;
            }

            var stockNameContainsTwoCodename = name.EndsWith(")");
            if (stockNameContainsTwoCodename)
            {
                var splitString = name.Split("(");
                var codename = splitString.First();
                var codenameIsIndexName = codename.StartsWith('^') || codename.Contains('.');

                return codenameIsIndexName ? PopularityItemType.ForeignIndex : PopularityItemType.Other;
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

            return PopularityItemType.Other;
        }
    }
}