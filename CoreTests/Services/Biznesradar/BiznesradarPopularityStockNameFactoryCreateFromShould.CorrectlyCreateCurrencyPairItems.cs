using System.Collections.Generic;
using Core.Model;
using Core.Services.Popularity;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    public class BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateCurrencyPairItems
    {
        private readonly BiznesradarPopularityStockNameFactory _sut;


        public BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateCurrencyPairItems()
        {
            var typeFactory = new Mock<IPopularityItemTypeFactory>();
            typeFactory.Setup(mock => mock.CreateTypeFrom(It.IsAny<string>())).Returns(PopularityItemType.Currency);

            _sut = new BiznesradarPopularityStockNameFactory(typeFactory.Object);
        }


        [Theory(DisplayName =
            "BiznesradarPopularityStockNameFactory.CreateFrom() should correctly create Stocknames for commodites")]
        [MemberData(nameof(CurrencyPairsData))]
        public void
            BiznesradarPopularityStockNameFactory_CreateFrom_should_correctly_create_StockNames_for_commodities(
                string name, StockName expectedStockName)
        {
            var actualStockName = _sut.CreateFrom(name);
            actualStockName.Should().Be(expectedStockName,
                                        $"created {nameof(StockName)} from name '{name}' should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CurrencyPairsData => new List<object[]>
        {
            new object[] {"USD/PLN 1:1 - dolar/złoty", new StockName("USD/PLN", "1:1 - dolar/złoty")},
            new object[] {"EUR/PLN 1:1 - euro/złoty", new StockName("EUR/PLN", "1:1 - euro/złoty")}
        };
    }
}