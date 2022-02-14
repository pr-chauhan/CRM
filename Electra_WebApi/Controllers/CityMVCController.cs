using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class CityMVCController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: CityMVC
        public ActionResult Index()
        {
            List<City> lstCity = new List<City>();
            client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
            var response = client.GetAsync("CityApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<City>>();
                display.Wait();
                lstCity = display.Result;
            }
            return View(lstCity);
        }
    }
}