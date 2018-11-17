using PaymentService.Models;
using PaymentService.Models.RequestResponseModel;
using System;
using Xunit;

namespace PaymentService.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void GenerateSampleJSON()
        {
            var req = new PaymentRequest();
            req.Payment = new Payment();
            req.Payment.Amount = 100.99;
            req.Payment.CreditCard = new CreditCard
            {
                CardHolderName = "Kha T Tran",
                ExpireDate = "07/22",
                CVV = "654",
                AccountNumber = "1234-5678-9012-3456"
            };

            var reqJson = Newtonsoft.Json.JsonConvert.SerializeObject(req);
        }
    }
}
