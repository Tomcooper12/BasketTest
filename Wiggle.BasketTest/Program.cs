using System;
using Wiggle.BasketTest.App;
using Wiggle.BasketTest.Data;

namespace Wiggle.BasketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup
            var consoleFeed = new ConsoleFeed();
            var app = new BasketApp(new BasketData(), consoleFeed);
            
            //select basket
            Console.WriteLine("Please select a basket number:");
            int basketId;
            bool gotInput = app.DisplayAndGet(out basketId);

            //loop until value has been found
            while (!gotInput)
            {
                Console.WriteLine("Sorry, we didn't catch that, please choose again:");
                gotInput = app.DisplayAndGet(out basketId);
            }

            //get total
            decimal? total = app.GetTotalForBasket(basketId);
            if(!total.HasValue)
            {
                Console.WriteLine("Unable to find your basket");
                return;
            }

            Console.WriteLine("Your total is " + total.Value.ToString("C") + " would you like to add a voucher? [Y/N]");
            var addVoucher = Console.ReadLine();
            if (addVoucher.ToLower() != "y") return;

            //get voucher
            Console.WriteLine("Please add your voucher:");
            string voucherCode = Console.ReadLine();




        }
    }
}
