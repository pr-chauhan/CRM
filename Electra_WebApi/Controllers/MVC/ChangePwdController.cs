﻿using EntityClass;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class ChangePwdController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            id = "ADMIN";
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<User_detail>(client, id.ToString(), WebApiApplication.staticVariables.UserDetailApiName);
            return View(lst);
        }


        [HttpPost]
        public ActionResult Edit(User_detail collection)
        {
            try
            {
                var test = WebApiApplication.objCommon.ExecutePut(client, collection, WebApiApplication.staticVariables.UserDetailApiName);
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