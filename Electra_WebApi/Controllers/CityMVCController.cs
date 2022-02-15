using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        // GET: CityMVC/Details/5
        public ActionResult Details(int id)
        {
            City lstCity = new City();
            client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
            var response = client.GetAsync("CityApi/"+id);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<City>();
                display.Wait();
                lstCity = display.Result;
            }
            return View(lstCity);
        }

        // GET: CityMVC/Create
        public ActionResult Create()
        {
            List<State> list = new List<State>();
            client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            var response = client.GetAsync("StateApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<State>>();
                display.Wait();
                list = display.Result;
            }
            ViewBag.CL = list;

            return View();
        }

        // POST: CityMVC/Create
        [HttpPost]
        public ActionResult Create(City collection)
        {
            try
            {
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PostAsJsonAsync("CityApi", collection);
                putdata.Wait();

                var test = putdata.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: CityMVC/Edit/5
        public ActionResult Edit(int id)
        {
            List<State> list = new List<State>();
            client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            var res = client.GetAsync("StateApi");
            res.Wait();

            var data = res.Result;
            if (data.IsSuccessStatusCode)
            {
                var display = data.Content.ReadAsAsync<List<State>>();
                display.Wait();
                list = display.Result;
            }
            ViewBag.CL = list;
            City lstCity = null;
            //client.BaseAddress = new Uri("https://localhost:44305/api/CityApi/");
            var response = client.GetAsync("CityApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<City>();
                display.Wait();
                lstCity = display.Result;
            }
            return View(lstCity);
        }

        // POST: CityMVC/Edit/5
        [HttpPost]
        public ActionResult Edit(City collection)//
        {
            try
            {
                // TODO: Add update logic here
                //City lstCity = new City();
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata =  client.PutAsJsonAsync("CityApi", collection);  
                putdata.Wait();

                var test = putdata.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: CityMVC/Delete/5
        public ActionResult Delete(int id)
        {
            List<State> list = new List<State>();
            //client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            //var res = client.GetAsync("StateApi");
            //res.Wait();

            //var data = res.Result;
            //if (data.IsSuccessStatusCode)
            //{
            //    var display = data.Content.ReadAsAsync<List<State>>();
            //    display.Wait();
            //    list = display.Result;
            //}
            //ViewBag.CL = list;
            City lstCity = null;
            client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
            var response = client.GetAsync("CityApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<City>();
                display.Wait();
                lstCity = display.Result;
            }
            return View(lstCity);
        }

        // POST: CityMVC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, City collection)
        {
            try
            {
                // TODO: Add delete logic here

                City lstCity = null;
                client.BaseAddress = new Uri("https://localhost:44305/api/CityApi/");
                var response = client.DeleteAsync("CityApi?id=" + id.ToString());
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }
    }
}
