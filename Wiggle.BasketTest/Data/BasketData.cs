using System;
using System.Collections.Generic;
using System.Linq;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest.Data
{
    public class BasketData : IBasketData
    {
        /* *
         * Dummy implementation to get saved baskets
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

            if(basketLookup[id] != null)
            {
                return basketLookup[id];
            }

            return new Basket();
        }

        public string GetDiscountCode(string code)
        {
            if (code == "XXX-XXX") return code;
            if (code == "YYY-YYY") return code;
            return String.Empty;
        }
    }
}