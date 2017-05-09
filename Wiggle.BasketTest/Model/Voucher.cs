namespace Wiggle.BasketTest.Model
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal MinSpend { get; set; }
        public int Type { get; set; }
        public Category Category { get; set; }
    }
}
