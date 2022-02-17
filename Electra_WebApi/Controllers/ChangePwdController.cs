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
    public class ChangePwdController : Controller
    {
        HttpClient client = new HttpClient();
        private CraModel db = new CraModel();
        // GET: ChangePwd
        public ActionResult Index()
        {
            return View();
        }
        //// GET: User_detail/Edit/5
        public ActionResult Edit(string id)
        {
            User_detail list = new User_detail();
            client.BaseAddress = new Uri("https://localhost:44305/api/UserDetailApi");
            var response = client.GetAsync("UserDetailApi?id=" + "ADMIN".ToString());
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

        // POST: User_detail/Edit/5
        [HttpPost]
        public ActionResult Edit(User_detail collection)
        {
            try
            {
                // TODO: Add update logic here

                collection.DoE = DateTime.Now;
                collection.DoM = DateTime.Now;
                collection.E_UserID = "admin";
                collection.M_UserID = "admin";
                client.BaseAddress = new Uri("https://localhost:44305/api/User_detailApi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var putdata = client.PutAsJsonAsync("UserdetailApi", collection);
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