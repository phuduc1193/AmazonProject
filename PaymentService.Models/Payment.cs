using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Models
{
    public class Payment
    {
        public double Amount { get; set; }
        public CreditCard CreditCard;
    }
}
