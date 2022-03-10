using eRestoran.Areas.Admin.ViewModels.PosaljiSMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nexmo.Api;
using Microsoft.Extensions.Configuration;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace eRestoran.Areas.Uposlenik.Controllers
{
    [Area("Uposlenik")]
    [Authorize(Roles = "Uposlenik")]
    public class SMSController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(MessageVM message)
        {
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                to ="+38762801986",
                text = message.Sadrzaj
            });
            return View("Confirmation");
        }
    }
}
