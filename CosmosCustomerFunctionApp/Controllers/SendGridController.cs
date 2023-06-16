using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace CosmosCustomerFunctionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendGridController : ControllerBase
    {
        // DENNA SKA NOG LIGGA MED I DET EGNA AZURE FUNCTION PROJEKTET

        //private static void Main()
        //{
        //    Execute().Wait();
        //}

        //static async Task Execute()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("NAME OF ENVIRONMNT VARIABLE F SENDGRID KEY HERE *** CosmosCustomerSendGridAPI");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("helena@gmail.com", "Helena");
        //    var subject = "Welcome to Cosmos!";
        //    var to = new EmailAddress("helena.sveding@gmail.com", "New Customer");
        //    var plainTextContent = "we hope you'll like it here";
        //    var htmlContent = "<strong>Join our Cosmos Community</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
    }
}
