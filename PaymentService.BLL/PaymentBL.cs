using Common.Models;
using PaymentService.DAL;
using PaymentService.Models.RequestResponseModel;
using System;
using System.Collections.Generic;

namespace PaymentService.BLL
{
    public class PaymentBL
    {
        private BankDA _bankDA;
        public PaymentBL()
        {
            _bankDA = new BankDA();
        }

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

            var creditCard = _bankDA.GetAccount(requestPayment.Payment.CreditCard.AccountNumber);
            var balance = _bankDA.GetBalance(requestPayment.Payment.CreditCard.AccountNumber);
            var requestCreditCard = requestPayment.Payment.CreditCard;

            if (requestPayment == null)
            {
                errors.Add(new Error { ErrorCode = "30", ErrorDescription = "PaymentRequest must be provided" });
            }

            if (requestPayment?.Payment == null)
            {
                errors.Add(new Error { ErrorCode = "31", ErrorDescription = "PaymentRequest.Payment must be provided" });
            }

            if (requestPayment.Payment?.CreditCard == null)
            {
                errors.Add(new Error { ErrorCode = "32", ErrorDescription = "PaymentRequest.Payment.CreditCard must be provided" });
            }

            if (requestPayment.Payment?.Amount == 0)
            {
                errors.Add(new Error { ErrorCode = "33", ErrorDescription = "PaymentRequest.Payment.Amount must be provided" });
            }

            if(creditCard == null)
            {
                errors.Add(new Error { ErrorCode = "34", ErrorDescription = "Credit card does not exist" });
            }
            else
            {
                //Validate Card Holder Name
                if (requestCreditCard.CardHolderName != creditCard.CardHolderName || requestCreditCard.CVV != creditCard.CVV || requestCreditCard.ExpireDate != creditCard.ExpireDate)
                {
                    errors.Add(new Error { ErrorCode = "35", ErrorDescription = "Credit card does not match" });
                }

                //Check if balance is sufficient for payment
                if (requestPayment.Payment.Amount > balance)
                {
                    errors.Add(new Error { ErrorCode = "36", ErrorDescription = "Balance is not sufficient for payment" });
                }
            }

            return errors.Count != 0 ? errors : null;
        }
    }
}
