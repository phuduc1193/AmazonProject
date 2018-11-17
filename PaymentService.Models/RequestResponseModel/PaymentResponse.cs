using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Models.RequestResponseModel
{
    public class PaymentResponse: ResponseBase
    {
        public Guid AuthCode { get; set; }
    }
}
