using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Electra_WebApi.Controllers
{
    public class ConsigneeMVCController : Controller
    {
        private HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            lst = lst.OrderBy(x => x.Consignee_Name).ToList();
            return View(lst);
            }
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
            ViewBag.CL = lst.OrderBy(x => x.City_Name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Consignee collection)
        {
            try
            {
                var lst = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
                ViewBag.CL = lst.OrderBy(x => x.City_Name).ToList();
                if (WebApiApplication.objCommon.ValidateValue<Consignee>("Consignee_Name", collection.Consignee_Name))
                {
                    ModelState.AddModelError(nameof(Consignee.Consignee_Name), "Duplicate Consignee is not allowed..!!");
                    return View(collection);
                }
                HttpClient client1 = new HttpClient();
                collection.E_UserID = Session["UserName"].ToString();
                collection.DoE = System.DateTime.Now;
                var test = WebApiApplication.objCommon.ExecutePost(client1, collection, WebApiApplication.staticVariables.ConsigneeApiName);
                if (test.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(collection);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            HttpClient client1 = new HttpClient();
            var l = WebApiApplication.objCommon.ExecuteIndex<City>(client1, WebApiApplication.staticVariables.CityApiName);
            ViewBag.CL = l.OrderBy(x => x.City_Name).ToList();
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(Consignee collection)
        {
            try
            {
                collection.M_UserID = Session["UserName"].ToString();
                collection.DoM = System.DateTime.Now;
                var test = WebApiApplication.objCommon.ExecutePut(client, collection, WebApiApplication.staticVariables.ConsigneeApiName);
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

        public ActionResult Delete(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Delete(int id, Consignee collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<Invoice>("Consignee_ID", id.ToString()))
                {
                    ModelState.AddModelError(nameof(Consignee.Consignee_ID), "Consignee is used in Invoice, So Consignee can't be delete..!!");
                    HttpClient htpc = new HttpClient();
                    var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(htpc, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
                    return View(lst);
                }
                var test = WebApiApplication.objCommon.ExecuteDeleteByID(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
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

        public string GetStateName(int city_id)
        {
            string res = city_id.ToString();
            var state = (from ct in WebApiApplication.db.Cities
                         join ss in WebApiApplication.db.States on ct.State_ID equals ss.State_ID
                         where ct.City_ID.Equals(city_id)
                         select new
                         {
                             StateName = ss.State_Name
                         }).ToList();
            return state[0].StateName.ToString();
        }
        public string GetConsigneeName(int Consignee_id)
        {
            return WebApiApplication.objCommon.GetConsigneeNameByID(Consignee_id);
        }

        public string GetCityName(int Consigneey_id)
        {
            var state = (from cg in WebApiApplication.db.Consignees
                         join ct in WebApiApplication.db.Cities on cg.City_ID equals ct.City_ID
                         where cg.Consignee_ID == Consigneey_id
                         select new
                         {
                             City = (ct.City_Name == null ? string.Empty : ct.City_Name)
                         }
                         ).ToList();

            return state[0].City;
        }
        public JsonResult GetConsigneeDetails(int Consigneey_id)
        {

            var state = WebApiApplication.db.Consignees.Where(x => x.Consignee_ID.Equals(Consigneey_id)).ToList();
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        public string GetConsigneeAddress(int Consigneey_id)
        {
            var address = (from cg in WebApiApplication.db.Consignees
                           join ct in WebApiApplication.db.Cities on cg.City_ID equals ct.City_ID
                           join st in WebApiApplication.db.States on ct.State_ID equals st.State_ID
                           where cg.Consignee_ID == Consigneey_id
                           select new
                           {
                               _address = cg.address == null ? string.Empty : cg.address,
                               _city = ct.City_Name == null ? string.Empty: ct.City_Name,
                               _state = st.State_Name == null ? string.Empty : st.State_Name,
                               //_range = cg.RAnge == null ? string.Empty : cg.RAnge,
                               //_division = cg.Division == null ? string.Empty : cg.Division,
                               _comm_rate = cg.commission_rate == null ? string.Empty :  cg.commission_rate,
                           }).ToList();


            string fulladdress = address[0]._address + ", " +
                address[0]._city + ", " +
                address[0]._state + 
                //", Range : " +
              //address[0]._range + ", Division: " +
              // address[0]._division +
               ", EximCode : " +
                address[0]._comm_rate;
            return fulladdress;
        }
    }
}