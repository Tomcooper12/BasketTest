namespace Wiggle.BasketTest.Model
{
    public class VoucherOperation
    {
        public Basket Basket { get; set; }
        public Voucher Voucher { get; set; }
        public bool VoucherApplied { get; set; }
        public string Message { get; set; }

    }
}
