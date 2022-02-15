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
    public class ItemMVCController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: ItemMVC
        public ActionResult Index()
        {
            List<Item> list = new List<Item>();
            client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
            var response = client.GetAsync("ItemApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Item>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }
       
        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            Item list = new Item();
            client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
            var response = client.GetAsync("ItemApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Item>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Item collection)
        {
            try
            {
                // TODO: Add insert logic here
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PostAsJsonAsync("ItemApi", collection);
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

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            Item list = new Item();
            client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
            var response = client.GetAsync("ItemApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Item>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Item collection)
        {
            try
            {
                // TODO: Add update logic here

                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PutAsJsonAsync("ItemApi", collection);
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

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            Item list = new Item();
            client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
            var response = client.GetAsync("ItemApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Item>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: Items/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Item collection)
        {
            try
            {
                // TODO: Add delete logic here
                client.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.DeleteAsync("ItemApi?id=" + id.ToString());
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