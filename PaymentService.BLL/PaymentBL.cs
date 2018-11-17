using Common.Models;
using PaymentService.Models.RequestResponseModel;
using System;
using System.Collections.Generic;

namespace PaymentService.BLL
{
    public class PaymentBL
    {
        public PaymentResponse AuthorizePayment(PaymentRequest requestPayment)
        {
            PaymentResponse responsePayment = new PaymentResponse();
            var errors = ValidatePaymentRequest(requestPayment);
            if (errors != null)
            {
                responsePayment.Errors = errors;
            }
            else
            {
                responsePayment.AuthCode = Guid.NewGuid();
            }

            return responsePayment;
        }

        private List<Error> ValidatePaymentRequest(PaymentRequest requestPayment)
        {
            List<Error> errors = new List<Error>();

            if (requestPayment == null)
            {
                errors.Add(new Error { ErrorCode = "1", ErrorDescription = "PaymentRequest must be provided" });
            }

            if (requestPayment?.Payment == null)
            {
                errors.Add(new Error { ErrorCode = "2", ErrorDescription = "PaymentRequest.Payment must be provided" });
            }

            if (requestPayment.Payment?.CreditCard == null)
            {
                errors.Add(new Error { ErrorCode = "3", ErrorDescription = "PaymentRequest.Payment.CreditCard must be provided" });
            }

            if (requestPayment.Payment?.Amount == null)
            {
                errors.Add(new Error { ErrorCode = "4", ErrorDescription = "PaymentRequest.Payment.Amount must be provided" });
            }

            return errors.Count != 0 ? errors : null;
        }
    }
}
