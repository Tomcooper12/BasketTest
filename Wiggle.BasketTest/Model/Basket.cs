using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiggle.BasketTest.Model
{
    public class Basket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalProducts { get; set; }
        public decimal Total { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Voucher> Vouchers { get; set; }

    }
}
