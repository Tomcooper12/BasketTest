using System;
using Wiggle.BasketTest.App;
using Wiggle.BasketTest.Data;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest
{
    class Program
    {
        static BasketData Data { get; set; }
        static BasketApp App { get; set; }

        static void setup()
        {
            Data = new BasketData();
            App = new BasketApp(Data, new ConsoleFeed());
        }
        static void Main(string[] args)
        {
            //setup
            setup();
            Basket basket;

            //select basket
            Console.WriteLine("Please select a basket number:");
            bool gotInput = App.DisplayAndGet(out basket);

            //loop until value has been found
            while (!gotInput)
            {
                Console.WriteLine("Sorry, we didn't catch that, please choose again:");
                gotInput = App.DisplayAndGet(out basket);
            }

            //get total
            basket = App.GetTotalForBasket(basket);
            if(basket == null)
            {
                Console.WriteLine("Unable to find your basket");
                return;
            }

            Console.WriteLine("Your total is " + basket.Total.ToString("C") + " would you like to add a voucher? [Y/N]");
            var addVoucher = Console.ReadLine();
            if (addVoucher.ToLower() != "y") return;

            //add voucher
            basket = VoucherProcess(basket);
            if(basket == null)
            {
                return;
            }

            Console.WriteLine("Your voucher has been applied, your total is " + basket.Total.ToString("C"));
            Console.WriteLine("Would you like to add another voucher? [Y/N]");
            var anotherVoucher = Console.ReadLine();
            if (anotherVoucher.ToLower() != "y") return;

            //add another voucher
            basket = VoucherProcess(basket);
            if (basket == null)
            {
                return;
            }

            Console.WriteLine("Your voucher has been applied, your total is " + basket.Total.ToString("C"));
            Console.Read();
        }

        static Basket VoucherProcess(Basket basket)
        {
            //setup
            setup();

            //get voucher
            Console.WriteLine("Please add your voucher:");
            string voucherCode = Console.ReadLine();
            var vouchers = Data.GetVoucherCodes(voucherCode);

            Voucher voucher;
            bool gotVoucher = App.DisplayAndGetVoucher(vouchers, out voucher);

            while (!gotVoucher)
            {
                Console.WriteLine("Sorry, we didn't catch that, please choose again:");
                gotVoucher = App.DisplayAndGetVoucher(vouchers, out voucher);
            }

            //add voucher
            var voucherOp = App.AddVoucherToBasket(new VoucherOperation
            {
                Basket = basket,
                Voucher = voucher
            });

            //check response
            if (!voucherOp.VoucherApplied)
            {
                Console.WriteLine(voucherOp.Message);
                Console.Read();
                return null;
            }

            return voucherOp.Basket;
        }
    }
}
