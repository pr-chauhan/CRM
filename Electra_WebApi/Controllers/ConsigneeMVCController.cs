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
    public class ConsigneeMVCController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: Consignees
        public ActionResult Index()
        {
            List<Consignee> list = new List<Consignee>();
            client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
            var response = client.GetAsync("ConsigneeApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Consignee>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // GET: Consignees/Details/5
        public ActionResult Details(int id)
        {
            Consignee list = new Consignee();
            client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
            var response = client.GetAsync("ConsigneeApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Consignee>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // GET: Consignees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consignees/Create
        [HttpPost]
        public ActionResult Create(Consignee collection)
        {
            try
            {
                // TODO: Add insert logic here

                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PostAsJsonAsync("ConsigneeApi", collection);
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

        // GET: Consignees/Edit/5
        public ActionResult Edit(int id)
        {
            Consignee list = new Consignee();
            client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
            var response = client.GetAsync("ConsigneeApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Consignee>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: Consignees/Edit/5
        [HttpPost]
        public ActionResult Edit(Consignee collection)
        {
            try
            {
                // TODO: Add update logic here

                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PutAsJsonAsync("ConsigneeApi", collection);
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

        // GET: Consignees/Delete/5
        public ActionResult Delete(int id)
        {
            Consignee list = new Consignee();
            client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
            var response = client.GetAsync("ConsigneeApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Consignee>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: Consignees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Consignee collection)
        {
            try
            {
                // TODO: Add delete logic here
                client.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.DeleteAsync("ConsigneeApi?id=" + id.ToString());
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
    }
}