using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Electra_WebApi.Controllers
{
    public class ConsigneeMVCController : Controller
    {
        private HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Consignee collection)
        {
            try
            {
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
                if (WebApiApplication.objCommon.ValidateValue<Consignee>("Consignee_Name", collection.Consignee_Name))
                {
                    ModelState.AddModelError(nameof(Consignee.Consignee_Name), "Duplicate Consignee is not allowed..!!");
                    return View(collection);
                }
                var test = WebApiApplication.objCommon.ExecutePost(client, collection, WebApiApplication.staticVariables.ConsigneeApiName);
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

        public ActionResult Edit(int id)
        {
            HttpClient client1 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<City>(client1, WebApiApplication.staticVariables.CityApiName);
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Consignee>(client, id.ToString(), WebApiApplication.staticVariables.ConsigneeApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(Consignee collection)
        {
            try
            {
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
            //return string.Empty;
        }
        public string GetConsigneeName(int Consignee_id)
        {
            return WebApiApplication.objCommon.GetConsigneeNameByID(Consignee_id);
        }
        //public string GetConsigneeAddress(int Consignee_id)
        //{
        //   // return WebApiApplication.objCommon.GetConsigneeAddressByID(Consignee_id);
        //}
        public string GetCityName(int Consigneey_id)
        {
           
            var state = (from cg in WebApiApplication.db.Consignees 
                         join ct in WebApiApplication.db.Cities on cg.City_ID equals ct.City_ID
                         where cg.Consignee_ID == Consigneey_id
                         select new
                         {
                             City = ct.City_Name
                         }
                         ).ToList();
            // do here some operation  
            return state[0].City;
        }
        public JsonResult GetConsigneeDetails(int Consigneey_id)
        {

            var state = WebApiApplication.db.Consignees.Where(x => x.Consignee_ID.Equals(Consigneey_id)).ToList();
                         
            // do here some operation  
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
                               _address = cg.address,
                               _city = ct.City_Name,
                               _state = st.State_Name,
                               _range = cg.RAnge,
                               _division = cg.Division,
                               _comm_rate = cg.commission_rate
                           }).ToList();


            string fulladdress = address[0]._address + ", " +
                address[0]._city + ", " +
                address[0]._state + ", Range : " +
              address[0]._range + ", Division: " +
               address[0]._division + ", Comm.Rate : " +
                address[0]._comm_rate;
            // do here some operation  
            return fulladdress;
        }
    }
}