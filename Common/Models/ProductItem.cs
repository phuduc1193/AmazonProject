namespace Common.Models
{
    public class ProductItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        //public Order Order { get; set; }
        public decimal UnitPrice { get; set; }
    }
}