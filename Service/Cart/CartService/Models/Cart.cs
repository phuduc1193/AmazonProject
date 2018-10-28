using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public string UserId { get; set; }
        public DateTime DateLastModified { get; set; }
        public enum CartStatus
        {
            Current, // This cart is the current cart that this user is shopping on
            Ordered, // This cart has been ordered
            Deleted, // This cart has been deleted
            Saved // This cart has been saved by the user and he/she has moved on to new cart
        }

        public List<CartItem> CartItems { get; set; }

    }
}
