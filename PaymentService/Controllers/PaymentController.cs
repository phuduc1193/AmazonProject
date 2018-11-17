using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.BLL;
using PaymentService.Models.RequestResponseModel;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpGet]
        public string GetAuthorize()
        {
            return "Hello";
        }

        [HttpPost("[action]")]
        public PaymentResponse AuthorizePayment([FromBody] PaymentRequest payment)
        {
            var paymentBL = new PaymentBL();
            var responsePayment = paymentBL.AuthorizePayment(payment);
            return responsePayment;
        }
    }
}