using System.Collections.Generic;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest.Data
{
    public interface IBasketData
    {
        List<Basket> GetBaskets();
        Basket GetBasket(int id);
        Voucher GetDiscountCode(string code);
        Dictionary<int, Voucher> GetVoucherCodes(string code);
    }
}