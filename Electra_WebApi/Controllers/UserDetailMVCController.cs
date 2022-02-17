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
    public class UserDetailMVCController : Controller
    {
        HttpClient client = new HttpClient();
        private CraModel db = new CraModel();
        // GET: UserMVC
        public ActionResult Index()
        {
            List<User_detail> list = new List<User_detail>();
            client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
            var response = client.GetAsync("UserDetailApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<User_detail>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }
        // GET: User_detail/Details/5
        public ActionResult Details(string id)
        {
            User_detail list = new User_detail();
            client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
            var response = client.GetAsync("UserDetailApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<User_detail>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // GET: User_detail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_detail/Create
        [HttpPost]
        public ActionResult Create(User_detail collection)
        {
            try
            {
                // TODO: Add insert logic here
                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = null;
                if (!Validate(collection.User_Name))
                {
                    ModelState.AddModelError(nameof(User_detail.User_Name), "Duplicate User Name is not allowed..!!");
                    return View(collection);

                }

                client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PostAsJsonAsync("UserDetailApi", collection);
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

        public ActionResult Delete(string id)
        {
            User_detail list = new User_detail();
            client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
            var response = client.GetAsync("UserDetailApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<User_detail>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        // POST: UserDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, User_detail collection)
        {
            try
            {
                // TODO: Add delete logic here

                var st = (from l in db.Invoices
                          where l.E_userid == id
                          select new
                          {
                              sName = l.E_userid
                          }).ToList();
                if (st.Count > 0)
                {
                    ModelState.AddModelError(nameof(User_detail.User_Name), "UserName is used in Invoice master, So UserName can't be delete..!!");
                    return View(collection);
                }


                client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.DeleteAsync("UserDetailApi?id=" + id.ToString());
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
            var st = (from l in db.User_detail
                      where l.User_Name == Parameter
                      select new
                      {
                          sName = l.User_Name
                      }).ToList();
            if (st.Count > 0)
            {
                lRetVal = false;
            }
            return lRetVal;
        }
    }
}