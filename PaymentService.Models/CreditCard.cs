using System;

namespace PaymentService.Models
{
    public class CreditCard
    {
        public string CardHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string ExpireDate { get; set; }
        public string CVV { get; set; }
        public double Balance { get; set; }
    }
}
