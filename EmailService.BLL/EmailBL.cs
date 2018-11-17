using Common.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EmailService.BLL
{
    public class EmailBL
    {
        private string PopulateConfirmationEmailBody(Order order)
        {
            var orderItems = string.Empty;
            var customerName = order.User.Name;
            var orderID = order.Id.ToString();
            var date = order.OrderDate.ToShortDateString();
            var totalprice = "$"+ order.TotalPrice.ToString();
      

            foreach(var item in order.Items)
            {
                orderItems += $"<p>{item.Product.Name}              ${item.UnitPrice}</p>";
            }

            string body = "";
            using (StreamReader reader = new StreamReader(@"F:\GitHub\AmazonProject\EmailService.BLL\HTML\OrderConfirmation.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{CustomerName}", customerName);
            body = body.Replace("{Date}", date);
            body = body.Replace("{OrderID}", orderID);
            body = body.Replace("{Items}", orderItems);
            body = body.Replace("{TotalPrice}", totalprice);
            
            return body;
        }

        public bool SendOrderConfirmationEmail(Order order)
        {
            string fromEmail = "soltran14@hotmail.com";
            string toEmail = order.User.Email;
            string subject = "Your Amazon.com order of {orderItems}";
            string body = PopulateConfirmationEmailBody(order);

            try
            {   
                using (MailMessage message = new MailMessage(fromEmail, toEmail, subject, body)
                    {
                        IsBodyHtml = true
                    }
                )
                {
                    SmtpClient client = new SmtpClient("smtp.live.com", 587)
                    {
                        UseDefaultCredentials = false,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("soltran14@hotmail.com", "trantran1420") //TODO: re-test with amazonclone14@gmail.com
                    };

                    //client.SendCompleted += Client_SendCompleted;

                    //client.SendCompleted += (object sender, System.ComponentModel.AsyncCompletedEventArgs e) =>
                    //{
                    //    if (e.Error != null)
                    //    {

                    //    }
                    //};

                    client.SendCompleted += (s, e) =>
                    {
                        if (e.Error != null)
                        {
                        }
                    };

                    client.Send(message);

                    

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
