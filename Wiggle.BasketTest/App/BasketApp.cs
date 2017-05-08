using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiggle.BasketTest.Data;

namespace Wiggle.BasketTest.App
{
    public class BasketApp
    {
        private readonly IBasketData Data;
        private readonly IUserFeed ConsoleFeed;
        public BasketApp(IBasketData data, IUserFeed console)
        {
            //dep inject
            Data = data;
            ConsoleFeed = console;
        }

        public bool DisplayAndGet(out int value)
        {
            value = 0;
            DisplayBaskets();
            var selection = Console.ReadLine();
            var choice = ParseBasketSelection(selection);
            if (choice.HasValue)
            {
                value = choice.Value;
            }
            return choice.HasValue;
        }

        public void DisplayBaskets()
        {
            var baskets = Data.GetBaskets();
            //list baskets
            for (int i = 0; i < baskets.Count; i++)
            {
                ConsoleFeed.WriteLine("[" + (i + 1) + "] " + baskets[i].Name);
            }
        }

        public int? ParseBasketSelection(string selection)
        {
            int basketId;
            //return
            if (!int.TryParse(selection, out basketId)) return (int?)null;
            return basketId;
        }

        public decimal? GetTotalForBasket(int basketId)
        {
            var basket = Data.GetBasket(basketId);
            if(basket.Products != null)
                return basket.Products.Sum(p => p.Price);
            return null;
        }
    }
}
