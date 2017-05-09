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
            Voucher voucher = basketData.GetDiscountCode(code);

            //assert
            Assert.Equal(voucher.Code, code);
        }

        [Fact]
        public void GetDiscountCode_ReturnsNull()
        {
            //act
            Voucher voucher = basketData.GetDiscountCode(string.Empty);

            //assert
            Assert.Null(voucher);
        }

        [Fact]
        public void GetDiscountCode_ReturnsGift()
        {
            //act
            Voucher voucher = basketData.GetDiscountCode("XXX-XXX");

            //assert
            Assert.Equal(voucher.Type, (int)VoucherType.Gift);
        }

        [Fact]
        public void GetDiscountCode_ReturnsOffer()
        {
            //act
            Voucher voucher = basketData.GetDiscountCode("YYY-YYY");

            //assert
            Assert.Equal(voucher.Type, (int)VoucherType.Offer);
        }

        [Theory]
        [InlineData("YYY-YYY", 2)]
        [InlineData("XXX-XXX", 1)]
        public void GetVoucherCodes_ReturnsCodes(string code, int expected)
        {
            //act
            var vouchers = basketData.GetVoucherCodes(code);

            //assert
            Assert.Equal(vouchers.Count, expected);
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
