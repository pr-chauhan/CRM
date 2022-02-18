using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using EntityClass;
namespace Electra_WebApi.Controllers
{
    public class StateMVCController : Controller
    {
        HttpClient client = new HttpClient();
        private CraModel db = new CraModel();
        // GET: States
        public ActionResult Index()
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
            return View(list);
        }

        // GET: States/Details/5
        public ActionResult Details(int id)
        {
            State list = new State();
            client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            var response = client.GetAsync("StateApi?id="+id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<State>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // GET: States/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        [HttpPost]
        public ActionResult Create(State collection)
        {
            try
            {
                // TODO: Add insert logic here
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = null;
                if (!Validate(collection.State_Name))
                {
                    ModelState.AddModelError(nameof(State.State_Name), "Duplicate state is not allowed..!!");
                    return View(collection);
                    
                }
              
                client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PostAsJsonAsync("StateApi", collection);
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

        // GET: States/Edit/5
        public ActionResult Edit(int id)
        {
            State list = new State();
            client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            var response = client.GetAsync("StateApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<State>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: States/Edit/5
        [HttpPost]
        public ActionResult Edit(State collection)
        {
            try
            {
                // TODO: Add update logic here

                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PutAsJsonAsync("StateApi", collection);
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

        // GET: States/Delete/5
        public ActionResult Delete(int id)
        {
            State list = new State();
            client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            var response = client.GetAsync("StateApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<State>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: States/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,State collection)
        {
            try
            {
                // TODO: Add delete logic here

                var st = (from l in db.Cities
                          where l.State_ID == id
                          select new
                          {
                              sName = l.State_ID
                          }).ToList();
                if (st.Count > 0)
                {
                    ModelState.AddModelError(nameof(State.State_ID), "State is used in city master, So state can't be delete..!!");
                    State list = new State();
                    HttpClient htpc = new HttpClient();
                    htpc.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
                    var response = htpc.GetAsync("StateApi?id=" + id.ToString());
                    response.Wait();

                    var testd = response.Result;
                    if (testd.IsSuccessStatusCode)
                    {
                        var display = testd.Content.ReadAsAsync<State>();
                        display.Wait();
                        list = display.Result;
                    }
                    return View(list);
                }


                client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.DeleteAsync("StateApi?id="+id.ToString());
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

        public bool  Validate(string Parameter)
        {
            bool lRetVal = true;
            //HttpClient _client = new HttpClient();
            //List<State> list = new List<State>();
            //_client.BaseAddress = new Uri("https://localhost:44305/api/StateApi");
            //var response = _client.GetAsync("StateApi");
            //response.Wait();

            //var test = response.Result;
            //if (test.IsSuccessStatusCode)
            //{
            //    var display = test.Content.ReadAsAsync<List<State>>();
            //    display.Wait();
            //    list = display.Result;
            //}

            var st = (from l in db.States
                      where l.State_Name == Parameter
                      select new
                      {
                          sName = l.State_Name
                      }).ToList();
            if(st.Count>0)
            {
                lRetVal= false;
            }
            return lRetVal;
        }

    }
}
