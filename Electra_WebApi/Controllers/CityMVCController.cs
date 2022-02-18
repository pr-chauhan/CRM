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
        private CraModel db = new CraModel();
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
           
            ViewBag.CL = GetStateList();

            return View();
        }

        public List<State> GetStateList()
        {
            List<State> list = new List<State>();
            HttpClient client = new HttpClient();
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
            return list;
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
                ViewBag.CL = GetStateList();
                if (!Validate(collection.City_Name))
                {
                   
                    ModelState.AddModelError(nameof(City.City_Name), "Duplicate City is not allowed..!!");
                    return View(collection);

                }
               
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
           
            ViewBag.CL = GetStateList();
            City lstCity = null;
            client.BaseAddress = new Uri("https://localhost:44305/api/CityApi/");
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
                ViewBag.CL = GetStateList();
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
                var st = (from l in db.Consignees
                          where l.City_ID == id
                          select new
                          {
                              sName = l.City_ID
                          }).ToList();
                if (st.Count > 0)
                {
                    ModelState.AddModelError(nameof(City.City_ID), "City is used in Consignees master, So city can't be delete..!!");

                    City lstCity = null;
                    HttpClient clientd = new HttpClient();
                    clientd.BaseAddress = new Uri("https://localhost:44305/api/CityApi");
                    var responsed = clientd.GetAsync("CityApi?id=" + id.ToString());
                    responsed.Wait();

                    var testd = responsed.Result;
                    if (testd.IsSuccessStatusCode)
                    {
                        var display = testd.Content.ReadAsAsync<City>();
                        display.Wait();
                        lstCity = display.Result;
                    }
                    return View(lstCity);
                }



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

        public bool Validate(string Parameter)
        {
            bool lRetVal = true;
           

            var st = (from l in db.Cities
                      where l.City_Name == Parameter
                      select new
                      {
                          sName = l.City_Name
                      }).ToList();
            if (st.Count > 0)
            {
                lRetVal = false;
            }
            return lRetVal;
        }

        public string GetCityName(int city_id)
        {
            var state = db.Cities.Where(x => x.City_ID.Equals(city_id)).ToList();
            // do here some operation  
            return state[0].City_Name.ToString();
        }
    }
}
