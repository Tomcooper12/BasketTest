using System.Collections.Generic;
using Wiggle.BasketTest.Model;

namespace Wiggle.BasketTest.Data
{
    public interface IBasketData
    {
        List<Basket> GetBaskets();
        Basket GetBasket(int id);
        string GetDiscountCode(string code);
    }
}