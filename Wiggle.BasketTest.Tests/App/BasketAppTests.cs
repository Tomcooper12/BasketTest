using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiggle.BasketTest.App;
using Wiggle.BasketTest.Data;
using Wiggle.BasketTest.Model;
using Xunit;

namespace Wiggle.BasketTest.Tests.App
{
    public class BasketAppTests
    {
        [Fact]
        public void DisplayBaskets_WritesOutput()
        {
            //arrange
            var consoleMock = new Mock<IUserFeed>();
            consoleMock.Setup(c => c.WriteLine(It.IsAny<string>()));
            var app = new BasketApp(new BasketData(), consoleMock.Object);

            //act
            app.DisplayBaskets();

            //assert
            consoleMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void GetBasketSelection_AsInt(string input, int expected)
        {
            //arrange
            var app = new BasketApp(new BasketData(), new ConsoleFeed());

            //act
            int? choice = app.ParseBasketSelection(input);

            //assert
            Assert.True(choice.HasValue);
            Assert.Equal(choice.Value, expected);
        }

        [Fact]
        public void GetBasketSelection_Null()
        {
            //arrange
            string userChoice = "test";
            var app = new BasketApp(new BasketData(), new ConsoleFeed());

            //act
            int? choice = app.ParseBasketSelection(userChoice);

            //assert
            Assert.False(choice.HasValue);
        }

        [Theory]
        [InlineData(1, 65.15d)]
        public void GetTotalForBasket_AsDecimal(int basketId, decimal expected)
        {
            //arrange
            var dataMock = new Mock<IBasketData>();
            dataMock.Setup(m => m.GetBasket(It.IsAny<int>())).Returns(new Basket
            {
                Name = "Basket 1",
                Id = 1,
                Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Hat",
                            Price = 10.50m,
                            Quantity = 1
                        },
                        new Product
                        {
                            Name = "Jumper",
                            Price = 54.65m,
                            Quantity = 1
                        }
                    }
            });
            var app = new BasketApp(dataMock.Object, new ConsoleFeed());

            //act
            var total = app.GetTotalForBasket(basketId);

            //assert
            Assert.Equal(expected, total);
        }

        [Fact]
        public void GetTotalForBasket_Zero()
        {
            //arrange
            var dataMock = new Mock<IBasketData>();
            dataMock.Setup(m => m.GetBasket(It.IsAny<int>())).Returns(new Basket());
            var app = new BasketApp(dataMock.Object, new ConsoleFeed());

            //act
            var total = app.GetTotalForBasket(It.IsAny<int>());

            //assert
            Assert.False(total.HasValue);
        }
    }
}
