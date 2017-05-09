using System;
using System.Collections.Generic;
using System.Linq;
using Wiggle.BasketTest.Data;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest.App
{
    internal class BasketApp
    {
        private const string _ONLY_ONE_OFFER = "You may only use one offer voucher per purchase";
        private const string _MUST_HAVE_HEADGEAR = "There are no products in your basket applicable to voucher {VOUCHER}.";
        private const string _MINIMUM_SPEND_OFFER = "You have not reached the spend threshold for voucher {VOUCHER}.Spend another {AMOUNT} to receive {DISCOUNT} discount from your basket total.";
        private const string _VOUCHER_APPLIED = "Voucher {VOUCHER} applied";
        private const string _VOUCHER_INVALID = "You have supplied and incorrect voucher";

        private readonly IBasketData Data;

        internal BasketApp(IBasketData data)
        {
            //dep inject
            Data = data;
        }

        internal bool DisplayAndGet(out Basket value)
        {
            DisplayBaskets();
            var selection = Console.ReadLine();
            var basket = ParseBasketSelection(selection);
            if(basket != null)
            {
                value = basket;
                return true;
            }
            value = null;
            return false;
        }

        internal bool DisplayAndGetVoucher(Dictionary<int, Voucher> vouchers, out Voucher value)
        {
            int voucherId;
            if(vouchers.Count == 1)
            {
                value = vouchers.Values.First();
                return true;
            }

            if (vouchers.Count > 1)
            {
                Console.WriteLine("We have found multiple matches, please select your voucher");
                foreach (var voucher in vouchers)
                {
                    Console.WriteLine("[" + voucher.Key + "] " + voucher.Value.Code + " " + voucher.Value.Description);
                }

                var voucherstr = Console.ReadLine();

                if(int.TryParse(voucherstr, out voucherId) && vouchers.ContainsKey(voucherId))
                {
                    value = vouchers[voucherId];
                    return true;
                }
            }
            value = null;
            return false;
        }

        internal Basket ParseBasketSelection(string selection)
        {
            int basketId;
            if (!int.TryParse(selection, out basketId)) return null;
            return Data.GetBasket(basketId);
        }

        internal Basket GetTotalForBasket(Basket basket)
        {
            if (basket == null || basket.Products == null) return null;
            //total products
            basket.TotalProducts = basket.Products
                .Where(p => p.Category.Name != "Voucher")
                .Sum(p => (p.Price * p.Quantity));

            //if vouchers applied
            if(basket.Vouchers != null && basket.Vouchers.Count > 0)
            {
                var headGearVoucher = basket.Vouchers.Where(v => v.Category.Name == "Headgear").FirstOrDefault();
                if (headGearVoucher != null) {
                    var headGearProducts = basket.Products.Where(p => p.Category.Name == "Headgear");
                    var headgearTotal = headGearProducts.Sum(p => (p.Price * p.Quantity));

                    if(headgearTotal < headGearVoucher.Discount)
                    {
                        basket.Total = ((basket.Products.Where(p => p.Category.Name != "Headgear")
                            .Sum(p => (p.Price * p.Quantity))) - (basket.Vouchers.Where(v => v.Category.Name != "Headgear").Sum(v => v.Discount)));
                        return basket;
                    }
                }

                //total products minus vouchers applied
                basket.Total = ((basket.Products.Sum(p => (p.Price * p.Quantity))) - (basket.Vouchers.Sum(v => v.Discount)));
                return basket;
            }

            //total products and vouchers
            basket.Total = basket.Products.Sum(p => (p.Price * p.Quantity));

            return basket;
        }

        internal VoucherOperation AddVoucherToBasket(VoucherOperation voucherOperation)
        {
            var basket = voucherOperation.Basket;
            var voucher = voucherOperation.Voucher;
            if(basket.Vouchers == null) basket.Vouchers = new List<Voucher>();

            if(basket == null)
            {
                voucherOperation.VoucherApplied = false;
                return voucherOperation;
            }

            if(voucher == null)
            {
                voucherOperation.VoucherApplied = false;
                voucherOperation.Message = _VOUCHER_INVALID;
                return voucherOperation;
            }

            if(voucher.Type == (int)VoucherType.Offer)
            {
                if(basket.Vouchers.Where(v => v.Type == (int)VoucherType.Offer).Count() > 0)
                {
                    voucherOperation.VoucherApplied = false;
                    voucherOperation.Message = _ONLY_ONE_OFFER;
                    return voucherOperation;
                }

                if(voucher.Category.Name == "Headgear" && basket.Products.Where(p => p.Category.Name == "Headgear").Count() == 0)
                {
                    voucherOperation.VoucherApplied = false;
                    voucherOperation.Message = _MUST_HAVE_HEADGEAR.Replace("{VOUCHER}", voucher.Code);
                    return voucherOperation;
                }

                if(basket.TotalProducts < voucher.MinSpend)
                {
                    voucherOperation.VoucherApplied = false;
                    decimal difference = (voucher.MinSpend - basket.TotalProducts);
                    string mes = _MINIMUM_SPEND_OFFER
                        .Replace("{VOUCHER}", voucher.Code)
                        .Replace("{AMOUNT}", difference.ToString("C"))
                        .Replace("{DISCOUNT}", voucher.Discount.ToString("C"));
                    voucherOperation.Message = mes;
                    return voucherOperation;
                }
            }

            //Add Voucher
            basket.Vouchers.Add(voucher);

            //Get total
            voucherOperation.Basket = this.GetTotalForBasket(basket);
            voucherOperation.VoucherApplied = true;
            voucherOperation.Message = _VOUCHER_APPLIED.Replace("{VOUCHER}", voucher.Code);
            return voucherOperation;

        }

        private void DisplayBaskets()
        {
            var baskets = Data.GetBaskets();
            //list baskets
            for (int i = 0; i < baskets.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + baskets[i].Name);
            }
        }
    }
}
