using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Models;
using Common.Models.RequestResponseModels;
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


            if (emailBL.SendOrderConfirmationEmail(order))
            {
                isSend = true;
            }
            return isSend;
        }


        //[HttpPost("[action]")]
        //public EmailResponse ConfirmOrder2([FromBody] EmailRequest emailRequest)
        //{
        //    var response = new EmailResponse();

        //    var sw = new Stopwatch();
        //    sw.Start();

        //    try
        //    {
        //        //TODO: call BLL
        //        //
        //    }
        //    catch (Exception ex)
        //    {
        //        // swtich ex
        //        //response.Errors = ...
        //    }
        //    finally
        //    {
        //        sw.Stop();
        //        response.ProcessTime = sw.ElapsedMilliseconds;
        //    }
        //    return response;
        //}

        //[HttpPost("[action]")]
        //public EmailResponse ConfirmOrder3([FromBody] EmailRequest emailRequest)
        //{
        //    var response = new EmailResponse();

        //    var sw = new Stopwatch();
        //    sw.Start();

        //    try
        //    {
        //        //TODO: call BLL
        //        //
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        sw.Stop();
        //        response.ProcessTime = sw.ElapsedMilliseconds;
        //    }
        //    return response;
        //}
    }
}