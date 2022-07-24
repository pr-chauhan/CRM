using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using Electra_WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http.Headers;


namespace Electra_WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly HttpClient client1 = new HttpClient();
        private readonly HttpClient client2 = new HttpClient();
        private readonly HttpClient client3 = new HttpClient();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.STATE =  WebApiApplication.objCommon.ExecuteIndex<State>(client, WebApiApplication.staticVariables.StateApiName).Count();
            ViewBag.SALE = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client1, WebApiApplication.staticVariables.InvoiceApiName).Count();
            ViewBag.CITY = WebApiApplication.objCommon.ExecuteIndex<City>(client2, WebApiApplication.staticVariables.CityApiName).Count();
            ViewBag.ITEM = WebApiApplication.objCommon.ExecuteIndex<Item>(client3, WebApiApplication.staticVariables.ItemApiName).Count();
            return View();
        }
    }
}
