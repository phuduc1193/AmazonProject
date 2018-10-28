namespace Common.Models
{
    public class Inventory
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int LowThreshold { get; set; }
        public int HighThreshold { get; set; }
    }
}
