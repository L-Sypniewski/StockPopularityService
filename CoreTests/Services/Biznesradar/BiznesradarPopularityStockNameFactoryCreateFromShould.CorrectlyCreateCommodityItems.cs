using System.Collections.Generic;
using Core.Model;
using Core.Services.Popularity;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    public class BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateCommodityItems
    {
        private readonly BiznesradarPopularityStockNameFactory _sut;


        public BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateCommodityItems()
        {
            var typeFactory = new Mock<IPopularityItemTypeFactory>();
            typeFactory.Setup(mock => mock.CreateTypeFrom(It.IsAny<string>())).Returns(PopularityItemType.Commodity);

            _sut = new BiznesradarPopularityStockNameFactory(typeFactory.Object);
        }


        [Theory(DisplayName =
            "BiznesradarPopularityStockNameFactory.CreateFrom() should correctly create Stocknames for commodites")]
        [MemberData(nameof(CommoditiesData))]
        public void
            BiznesradarPopularityStockNameFactory_CreateFrom_should_correctly_create_StockNames_for_commodities(
                string name, StockName expectedStockName)
        {
            var actualStockName = _sut.CreateFrom(name);
            actualStockName.Should().Be(expectedStockName,
                                        $"created {nameof(StockName)} from name '{name}' should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CommoditiesData => new List<object[]>
        {
            new object[] {"Brent Crude Oil Spot - Ropa Brent", new StockName("Ropa Brent", "Brent Crude Oil Spot")},
            new object[] {"WTI Light Crude Oil Spot - Ropa naftowa", new StockName("Ropa naftowa", "WTI Light Crude Oil Spot")},
            new object[] {"Natural Gas Futures - Gaz ziemny", new StockName("Gaz ziemny", "Natural Gas Futures")}
        };
    }
}