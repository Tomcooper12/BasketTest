using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiggle.BasketTest.App;
using Wiggle.BasketTest.Data;
using Wiggle.BasketTest.Model;
using FluentAssertions;
using Xunit;

namespace Wiggle.BasketTest.Tests.App
{
    public class BasketAppTests
    {
        private Basket _Basket { get; set; }

        public BasketAppTests()
        {
            //setup
            _Basket = new Basket
            {
                Name = "Basket 1",
                Id = 1,
                Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Hat",
                            Price = 10.50m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        },
                        new Product
                        {
                            Name = "Jumper",
                            Price = 54.65m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        }
                    }
            };
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void ParseBasketSelection_AsInt(string input, int expected)
        {
            //arrange
            var mock = new Mock<IBasketData>();
            mock.Setup(m => m.GetBasket(It.IsAny<int>())).Returns(new Basket
            {
                Id = expected
            });
            var app = new BasketApp(mock.Object);

            //act
            Basket choice = app.ParseBasketSelection(input);

            //assert
            Assert.NotNull(choice);
            Assert.Equal(choice.Id, expected);
        }

        [Fact]
        public void ParseBasketSelection_Null()
        {
            //arrange
            var mock = new Mock<IBasketData>();
            mock.Setup(m => m.GetBasket(It.IsAny<int>())).Returns<Basket>(null);
            var app = new BasketApp(mock.Object);

            //act
            Basket choice = app.ParseBasketSelection(It.IsAny<string>());

            //assert
            Assert.Null(choice);
        }

        [Theory]
        [InlineData(65.15d)]
        public void GetTotalForBasket_AsDecimal(decimal expected)
        {
            //arrange
            var app = new BasketApp(new BasketData());

            //act
            var basket = app.GetTotalForBasket(_Basket);

            //assert
            Assert.Equal(expected, basket.Total);
        }

        [Fact]
        public void GetTotalForBasket_HasVoucherPurchase()
        {
            //arrange
            var basket = new Basket
            {
                Name = "Basket 5",
                Id = 5,
                Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Hat",
                            Price = 25.00m,
                            Quantity = 1,
                            Category = new Category { Name = "MISC" }
                        },
                        new Product
                        {
                            Name = "£30 Gift Voucher",
                            Price = 30.00m,
                            Quantity = 1,
                            Category = new Category {Name = "Voucher" }
                        }
                    }
            };
            var app = new BasketApp(new BasketData());

            //act
            basket = app.GetTotalForBasket(basket);

            //assert
            basket.Total.ShouldBeEquivalentTo(55.00d);
            basket.TotalProducts.ShouldBeEquivalentTo(25.00d);
        }

        [Fact]
        public void GetTotalForBasket_Zero()
        {
            //arrange
            var app = new BasketApp(new BasketData());

            //act
            var basket = app.GetTotalForBasket(It.IsAny<Basket>());

            //assert
            Assert.Null(basket);
        }

        [Theory]
        [InlineData("XXX-XXX", 5.00, (int)VoucherType.Gift, 60.15)]
        [InlineData("XXX-XXX", 10.00, (int)VoucherType.Gift, 55.15)]
        [InlineData("XXX-XXX", 30.00, (int)VoucherType.Gift, 35.15)]
        public void GetTotalForBasket_GiftVoucher(string code, decimal discount, int type, decimal expected)
        {
            //arrange
            Basket basket = new Basket
            {
                Name = "Basket 1",
                Id = 1,
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Hat",
                        Price = 10.50m,
                        Quantity = 1,
                        Category =  new Category { Name = "MISC" }
                    },
                    new Product
                    {
                        Name = "Jumper",
                        Price = 54.65m,
                        Quantity = 1,
                        Category =  new Category { Name = "MISC" }
                    }
                },
                Vouchers = new List<Voucher>
                {
                    new Voucher
                    {
                        Code = code,
                        Discount = discount,
                        Type = type,
                        Category = new Category {Name = "Products" }
                    }
                }
            };
            var app = new BasketApp(new BasketData());

            //act
            basket = app.GetTotalForBasket(basket);

            //Assert
            basket.Total.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void AddVoucherToBasket_VoucherAdded()
        {
            //arrange
            var app = new BasketApp(new BasketData());
            var voucher = new Voucher
            {
                Code = "XXX-XXX",
                Discount = 5.00m,
                Type = (int)VoucherType.Gift,
                Category = new Category { Name = "Products" }
            };

            //act
            var voucherOperation = app.AddVoucherToBasket(new VoucherOperation
            {
                Basket = _Basket,
                Voucher = voucher
            });

            //assert
            voucherOperation.VoucherApplied.Should().BeTrue();
            voucherOperation.Basket.Vouchers.Should().HaveCount(1);
            voucherOperation.Basket.Total.ShouldBeEquivalentTo(60.15);
        }
    }
}
