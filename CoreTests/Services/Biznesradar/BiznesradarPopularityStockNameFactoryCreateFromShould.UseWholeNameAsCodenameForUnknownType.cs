using System.Collections.Generic;
using Core.Model;
using Core.Services.Popularity;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    public class BiznesradarPopularityStockNameFactoryCreateFromShouldUseWholeNameAsCodenameForUnknownType
    {
        private readonly BiznesradarPopularityStockNameFactory _sut;


        public BiznesradarPopularityStockNameFactoryCreateFromShouldUseWholeNameAsCodenameForUnknownType()
        {
            var typeFactory = new Mock<IPopularityItemTypeFactory>();
            typeFactory.Setup(mock => mock.CreateTypeFrom(It.IsAny<string>())).Returns(( PopularityItemType? ) null);

            _sut = new BiznesradarPopularityStockNameFactory(typeFactory.Object);
        }


        [Theory(DisplayName =
            "BiznesradarPopularityStockNameFactory.CreateFrom() should create codename from whole name for unknown types")]
        [MemberData(nameof(CommoditiesData))]
        public void
            BiznesradarPopularityStockNameFactory_CreateFrom_should__create_codename_from_whole_name_for_unknown_types(
                string name, StockName expectedStockName)
        {
            var actualStockName = _sut.CreateFrom(name);
            actualStockName.Should().Be(expectedStockName,
                                        $"created {nameof(StockName)} from name '{name}' should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CommoditiesData => new List<object[]>
        {
            new object[] {"Brent Crude Oil Spot - Ropa Brent", new StockName("Brent Crude Oil Spot - Ropa Brent")},
            new object[] {"WTI Light Crude Oil Spot - Ropa naftowa", new StockName("WTI Light Crude Oil Spot - Ropa naftowa")},
            new object[] {"Natural Gas Futures - Gaz ziemny", new StockName("Natural Gas Futures - Gaz ziemny")},
            new object[] {"PKN (PKN Orlen)", new StockName("PKN (PKN Orlen)")},
            new object[] {"DAX.20", new StockName("DAX.20")},
            new object[] {"USD/PLN 1:1 - dolar/złoty", new StockName("USD/PLN 1:1 - dolar/złoty")},
            new object[] {"^SP500 (USA)", new StockName("^SP500 (USA)")}
        };
    }
}