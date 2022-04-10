using EntityClass;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class CityMVCController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                var lst = WebApiApplication.objCommon.ExecuteIndex<City>(client, WebApiApplication.staticVariables.CityApiName);
            lst = lst.OrderByDescending(x => x.City_ID).ToList();
            return View(lst);
            }
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        } 

        public ActionResult Create()
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<State>(client, WebApiApplication.staticVariables.StateApiName);
            ViewBag.CL = lst.OrderBy(x => x.State_Name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(City collection)
        {
            try
            {
                HttpClient clientNew = new HttpClient();
                var lst = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
                ViewBag.CL = lst.OrderBy(x => x.State_Name).ToList();
                if (WebApiApplication.objCommon.ValidateValue<City>("City_Name", collection.City_Name))
                {
                     ModelState.AddModelError(nameof(City.City_Name), "Duplicate City is not allowed..!!");
                    return View(collection);
                }
                collection.E_UserID = Session["UserName"].ToString();
                collection.DoE = System.DateTime.Now;
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
            var l = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
            ViewBag.CL = l.OrderBy(x => x.State_Name).ToList();
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id.ToString(), WebApiApplication.staticVariables.CityApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(City collection)
        {
            try
            {
                HttpClient clientNew = new HttpClient();
                var lst = WebApiApplication.objCommon.ExecuteIndex<State>(clientNew, WebApiApplication.staticVariables.StateApiName);
                ViewBag.CL = lst.OrderBy(x => x.State_Name).ToList();
                collection.M_UserID = Session["UserName"].ToString();
                collection.DoM = System.DateTime.Now;
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
