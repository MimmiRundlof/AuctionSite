using Moq;
using NackowskisApp.BusinessLayer;
using NackowskisApp.DataLayer;
using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestCases
{
    public class TestCases
    {
        private IChartBusiness _chartBusiness;

        private IAuctionBusiness _auctionBusiness;


        [Fact]
        public void ChartCalculatorTest()
        {
            var mock = new Mock<IChartBusiness>();

            
            mock.Setup(x => x.CalculateChart(2, 2)).Returns(new int[] { 2, 2, 0 });

            this._chartBusiness = mock.Object;

            //Act
            var result = _chartBusiness.CalculateChart(2, 2);

            //Assert
            Assert.Equal(new int[] { 2, 2, 0 }, result);
        }
        [Fact]
        public void GetAllAuctionsTest()
        {
            var mock = new Mock<IAuctionBusiness>();

            var expected = new List<Auction>()
            {
                new Auction()
            {
                AuktionID = 1,
                Beskrivning = "Descr",
                Titel = "Title",
                Utropspris = 1,
                SlutDatum = "2018-10-19",
                SkapadAv = "1234"
            },
                new Auction()
            {
                AuktionID = 2,
                Beskrivning = "Descr2",
                Titel = "Title2",
                Utropspris = 1,
                SlutDatum = "2018-10-09",
                SkapadAv = "12345"
            }

        };

            mock.Setup(x => x.GetAuctionsAsync()).ReturnsAsync(expected);

            this._auctionBusiness = mock.Object;

            //Act
            var actual = _auctionBusiness.GetAuctionsAsync().Result;

            //Assert
            Assert.Same(expected, actual);

        }
        [Fact]
        public void DeleteAuctionTest()
        {
            var mock = new Mock<IAuctionBusiness>();

            mock.Setup(x => x.DeleteAuctionAsync(It.IsAny<int>())).ReturnsAsync(true);

            this._auctionBusiness = mock.Object;

            //Act
            var actual = _auctionBusiness.DeleteAuctionAsync(1).Result;

            //Assert
            Assert.True(actual);

        }
      


    }
}





