﻿using EntityClass;
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
        private CraModel db = new CraModel();
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
            ViewBag.CL = GetCityList();
            return View();
        }
        public List<City> GetCityList()
        {
            List<City> list = new List<City>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
            var response = client.GetAsync("CityApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<City>>();
                display.Wait();
                list = display.Result;
            }
            return list;
        }
        // POST: Consignees/Create
        [HttpPost]
        public ActionResult Create(Consignee collection)
        {
            try
            {
                // TODO: Add insert logic here
                ViewBag.CL = GetCityList();
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                if (!Validate(collection.Consignee_Name))
                {
                    ModelState.AddModelError(nameof(Consignee.Consignee_Name), "Duplicate Consignee is not allowed..!!");
                    return View(collection);

                }
              
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

            ViewBag.CL = GetCityList();
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

                var st = (from l in db.Invoices
                          where l.Consignee_ID == id
                          select new
                          {
                              sName = l.Consignee_ID
                          }).ToList();
                if (st.Count > 0)
                {
                    ModelState.AddModelError(nameof(Consignee.Consignee_ID), "Consignee is used in Invoice, So Consignee can't be delete..!!");
                    Consignee list = new Consignee();
                    HttpClient htcp = new HttpClient();
                    htcp.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
                    var response = htcp.GetAsync("ConsigneeApi?id=" + id.ToString());
                    response.Wait();

                    var testd = response.Result;
                    if (testd.IsSuccessStatusCode)
                    {
                        var display = testd.Content.ReadAsAsync<Consignee>();
                        display.Wait();
                        list = display.Result;
                    }
                    return View(list);
                }

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

        public bool Validate(string Parameter)
        {
            bool lRetVal = true;
           

            var st = (from l in db.Consignees
                      where l.Consignee_Name == Parameter
                      select new
                      {
                          sName = l.Consignee_Name
                      }).ToList();
            if (st.Count > 0)
            {
                lRetVal = false;
            }
            return lRetVal;
        }

        public string GetStateName( int city_id)
        {
            string res = city_id.ToString();

            var state = (from ct in db.Cities
                      join ss in db.States on ct.State_ID equals ss.State_ID
                      where ct.City_ID.Equals(city_id)
                      select new
                      {
                          StateName = ss.State_Name
                      }).ToList();
                      
            // do here some operation  
            return state[0].StateName.ToString();
        }

    }
}