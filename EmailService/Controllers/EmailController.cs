using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Models;
using EmailService.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [HttpPost("[action]")]
        public bool ConfirmOrder([FromBody] Order order)
        {
            bool isSend = false;
            EmailBL emailBL = new EmailBL();

            var httpResponse = new HttpResponseMessage();

            if (httpResponse.IsSuccessStatusCode)
            {
                if (emailBL.SendOrderConfirmationEmail(order))
                {
                    isSend = true;
                }
            }
            else
            {
                isSend = false;
            }

            return isSend;
        }
    }
}