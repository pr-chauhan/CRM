using EntityClass;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class CityMVCController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        } 

        public ActionResult Create()
        {
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<State>(client, WebApiApplication.staticVariables.StateApiName);
            return View();
        }

        [HttpPost]
        public ActionResult Create(City collection)
        {
            try
            {
                HttpClient clientNew = new HttpClient();
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
                if(WebApiApplication.objCommon.ValidateValue<City>("City_Name", collection.City_Name))
                {
                     ModelState.AddModelError(nameof(City.City_Name), "Duplicate City is not allowed..!!");
                    return View(collection);
                }
                var test = WebApiApplication.objCommon.ExecutePost(client,collection, WebApiApplication.staticVariables.CityApiName);
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
            HttpClient clientNew = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(City collection)
        {
            try
            {
                HttpClient clientNew = new HttpClient();
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
                var test = WebApiApplication.objCommon.ExecutePut(client, collection, WebApiApplication.staticVariables.CityApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Delete(int id, City collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<Consignee>("City_ID", id.ToString()))
                {
                    ModelState.AddModelError(nameof(City.City_ID), "City is used in Consignees master, So city can't be delete!!");
                    HttpClient htpc = new HttpClient();
                    var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(htpc, id.ToString(), WebApiApplication.staticVariables.CityApiName);
                    return View(lst);
                }
                var test = WebApiApplication.objCommon.ExecuteDeleteByID(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
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

        public string GetCityName(int city_id)
        {
            return WebApiApplication.objCommon.GetCityNameByID(city_id);
        }
    }
}
