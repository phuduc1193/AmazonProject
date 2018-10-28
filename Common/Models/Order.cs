using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<ProductItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
