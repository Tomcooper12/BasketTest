using Moq;
using System;
using Wiggle.BasketTest.Data;
using Wiggle.BasketTest.Model;
using Xunit;

namespace Wiggle.BasketTest.Tests
{
    public class BasketDataTests
    {
        private readonly BasketData basketData;
        private readonly IUserFeed consoleFeed;

        public BasketDataTests()
        {
            //arrange
            basketData = new BasketData();

            var consoleMock = new Mock<IUserFeed>();
            consoleMock.Setup(m => m.ReadLine()).Returns(string.Empty);
            consoleFeed = consoleMock.Object;
        }

        [Fact]
        public void GetBaskets_NotNull()
        {
            //act
            var baskets = basketData.GetBaskets();

            //assert
            Assert.NotNull(baskets);
        }

        [Theory]
        [InlineData(5)]
        public void GetBaskets_Has3Baskets(int basketCount)
        {
            //act
            var baskets = basketData.GetBaskets();

            //assert
            Assert.True(baskets.Count == basketCount);
        }

        [Theory]
        [InlineData(1, "Basket 1")]
        [InlineData(2, "Basket 2")]
        [InlineData(3, "Basket 3")]
        public void GetBasket_ReturnsABasket(int id, string name)
        {
            //act
            Basket basket = basketData.GetBasket(id);

            //assert
            Assert.Equal(basket.Name, name);
            Assert.Equal(basket.Id, id);
        }

        [Theory]
        [InlineData("XXX-XXX")]
        [InlineData("YYY-YYY")]
        public void GetDiscountCode_ReturnsCode(string code)
        {
            //act
            string discountCode = basketData.GetDiscountCode(code);

            //assert
            Assert.True(discountCode == code);
        }

        [Fact]
        public void GetDiscountCode_ReturnsEmpty()
        {
            //act
            string code = basketData.GetDiscountCode(string.Empty);

            //assert
            Assert.True(String.IsNullOrEmpty(code));
        }

        [Fact]
        public void ConsoleFeed_ReturnsEmpty()
        {
            //act
            var test = consoleFeed.ReadLine();

            //assert
            Assert.True(String.IsNullOrEmpty(test));
        }

    }
}
