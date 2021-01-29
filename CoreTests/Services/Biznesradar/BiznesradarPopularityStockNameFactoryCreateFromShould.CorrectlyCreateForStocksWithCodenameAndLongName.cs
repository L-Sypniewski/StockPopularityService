using System.Collections.Generic;
using Core.Model;
using Core.Services.Popularity;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services.Biznesradar
{
    public class BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateForStocksWithCodenameAndLongName
    {
        private readonly BiznesradarPopularityStockNameFactory _sut;


        public BiznesradarPopularityStockNameFactoryCreateFromShouldCorrectlyCreateForStocksWithCodenameAndLongName()
        {
            var typeFactory = new Mock<IPopularityItemTypeFactory>();
            typeFactory.Setup(mock => mock.CreateTypeFrom(It.IsAny<string>())).Returns(PopularityItemType.Stock);

            _sut = new BiznesradarPopularityStockNameFactory(typeFactory.Object);
        }


        [Theory(DisplayName =
            "BiznesradarPopularityStockNameFactory.CreateFrom() should correctly create Stocknames for Stocks with codename and long name")]
        [MemberData(nameof(CodenameAndLongNameData))]
        public void
            BiznesradarPopularityStockNameFactory_CreateFrom_should_correctly_create_StockNames_for_Stocks_with_codename_and_long_name(
                string name, StockName expectedStockName)
        {
            var actualStockName = _sut.CreateFrom(name);
            actualStockName.Should().Be(expectedStockName,
                                        $"created {nameof(StockName)} from name '{name}' should be equal to {expectedStockName}");
        }


        public static IEnumerable<object[]> CodenameAndLongNameData => new List<object[]>
        {
            new object[] {"CDR (CDPROJEKT)", new StockName("CDR", "CDPROJEKT")},
            new object[] {"MVP (MARVIPOL)", new StockName("MVP", "MARVIPOL")},
            new object[] {"UNT (UNIMOT)", new StockName("UNT", "UNIMOT")},
            new object[] {"PCX (PCCEXOL)", new StockName("PCX", "PCCEXOL")},
            new object[] {"AWM (AIRWAY)", new StockName("AWM", "AIRWAY")},
        };
    }
}