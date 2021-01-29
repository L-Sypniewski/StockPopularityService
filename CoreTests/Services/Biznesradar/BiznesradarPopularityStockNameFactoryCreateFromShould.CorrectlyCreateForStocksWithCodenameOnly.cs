using System.Collections.Generic;
using Core.Model;
using Core.Services.Popularity;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    public class BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateForStocksWithCodenameOnly
    {
        private readonly BiznesradarPopularityStockNameFactory _sut;


        public BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateForStocksWithCodenameOnly()
        {
            var typeFactory = new Mock<IPopularityItemTypeFactory>();
            typeFactory.Setup(mock => mock.CreateTypeFrom(It.IsAny<string>())).Returns(PopularityItemType.Stock);

            _sut = new BiznesradarPopularityStockNameFactory(typeFactory.Object);
        }


        [Theory(DisplayName =
            "BiznesradarPopularityStockNameFactory.CreateFrom() should correctly create Stocknames for Stocks with codename only")]
        [MemberData(nameof(CodenameOnlyData))]
        public void
            BiznesradarPopularityStockNameFactory_CreateFrom_should_correctly_create_StockNames_for_Stocks_with_codename_only(
                string name, StockName expectedStockName)
        {
            var actualStockName = _sut.CreateFrom(name);
            actualStockName.Should().Be(expectedStockName,
                                        $"created {nameof(StockName)} from name '{name}' should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameOnlyData => new List<object[]>
        {
            new object[] {"JSW", new StockName("JSW")},
            new object[] {"IDH", new StockName("IDH")},
            new object[] {"PZU", new StockName("PZU")}
        };
    }
}