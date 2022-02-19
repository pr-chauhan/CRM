using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class InvoiceMVCController : Controller
    {
        HttpClient client = new HttpClient();
        private CraModel db = new CraModel();
        // GET: InvoiceMVC
        public ActionResult Index()
        {
            List<Invoice> list = new List<Invoice>();
            client.BaseAddress = new Uri("https://localhost:44305/api/InvoiceApi");
            var response = client.GetAsync("InvoiceApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Invoice>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }
    }
}