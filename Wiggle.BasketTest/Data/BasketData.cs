using System.Collections.Generic;
using System.Linq;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest.Data
{
    public class BasketData : IBasketData
    {
        /* *
         * Dummy implementation to get saved baskets
         * Opted to provide a list of saved baskets for the senario given
         * This was to avoid having to enumerate a list of products and fill a basket in a console.
         * */
        public List<Basket> GetBaskets()
        {
            return new List<Basket>
            {
                new Basket
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
                },
                new Basket
                {
                    Name = "Basket 2",
                    Id = 2,
                    Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Hat",
                            Price = 25.00m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        },
                        new Product
                        {
                            Name = "Jumper",
                            Price = 26.00m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        }
                    }
                },
                new Basket
                {
                    Name = "Basket 3",
                    Id = 3,
                    Products = new List<Product>
                    {
                        new Product
                        {
                            Name = "Hat",
                            Price = 25.00m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        },
                        new Product
                        {
                            Name = "Jumper",
                            Price = 26.00m,
                            Quantity = 1,
                            Category =  new Category { Name = "MISC" }
                        },
                        new Product
                        {
                            Name = "Head Light",
                            Price = 3.50m,
                            Quantity = 1,
                            Category =  new Category { Name = "Headgear" }
                        }
                    }
                },
                new Basket
                {
                    Name = "Basket 4",
                    Id = 4,
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
                            Name = "Hat",
                            Price = 26.00m,
                            Quantity = 1,
                            Category = new Category { Name = "MISC" }
                        }
                    }
                },
                new Basket
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
                }
            };
        }

        public Basket GetBasket(int id)
        {
            var baskets = GetBaskets();
            Dictionary<int, Basket> basketLookup = baskets.ToDictionary(x => x.Id);

            if (basketLookup[id] != null)
            {
                return basketLookup[id];
            }

            return null;
        }

        public Voucher GetDiscountCode(string code)
        {
            if (code == "xxx-xxx")
            {
                return new Voucher
                {
                    Id = 1,
                    Code = code,
                    Discount = 5.00m,
                    Type = (int)VoucherType.Gift,
                    Category = new Category { Name = "Products" }
                };
            }
            if (code == "yyy-yyy")
            {
                return new Voucher
                {
                    Id = 2,
                    Code = code,
                    Discount = 5.00m,
                    Type = (int)VoucherType.Offer,
                    Category = new Category { Name = "Headgear" }
                };
            }
            return null;
        }

        public Dictionary<int, Voucher> GetVoucherCodes(string code)
        {
            var table = new Dictionary<int, Voucher>();
            if (code == "xxx-xxx")
            {
                table.Add(1, new Voucher
                {
                    Id = 1,
                    Code = code,
                    Discount = 5.00m,
                    MinSpend = 50.00m,
                    Type = (int)VoucherType.Gift,
                    Category = new Category { Name = "Products" }
                });
                return table;
            }

            if (code == "yyy-yyy")
            {
                table.Add(2, new Voucher
                {
                    Id = 2,
                    Code = code,
                    Description = "£5.00 off Head Gear in baskets over £50.00",
                    Discount = 5.00m,
                    MinSpend = 50.00m,
                    Type = (int)VoucherType.Offer,
                    Category = new Category { Name = "Headgear" }
                });

                table.Add(3, new Voucher
                {
                    Id = 3,
                    Code = code,
                    Description = "£5.00 off baskets over £50.00",
                    Discount = 5.00m,
                    MinSpend = 50.00m,
                    Type = (int)VoucherType.Offer,
                    Category = new Category { Name = "Products" }
                });
                return table;
            }
            return null;
        }
    }
}