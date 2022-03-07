using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using EntityClass;
namespace Electra_WebApi.Controllers
{
    public class StateMVCController : Controller
    {
        private HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
            var lst = WebApiApplication.objCommon.ExecuteIndex<State>(client, WebApiApplication.staticVariables.StateApiName);
            lst = lst.OrderBy(x => x.State_Name).ToList();
            return View(lst);
            //}
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<State>(client, id.ToString(), WebApiApplication.staticVariables.StateApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(State collection)
        {
            try
            {
                client.BaseAddress = null;
                if (WebApiApplication.objCommon.ValidateValue<State>("State_Name", collection.State_Name))
                {
                    ModelState.AddModelError(nameof(State.State_Name), "Duplicate state is not allowed..!!");
                    return View(collection);
                }
                var test = WebApiApplication.objCommon.ExecutePost(client, collection, WebApiApplication.staticVariables.StateApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<State>(client, id.ToString(), WebApiApplication.staticVariables.StateApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(State collection)
        {
            try
            {
                var test = WebApiApplication.objCommon.ExecutePut(client, collection, WebApiApplication.staticVariables.StateApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<State>(client, id.ToString(), WebApiApplication.staticVariables.StateApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Delete(int id, State collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<City>("State_ID", id.ToString()))
                {
                    ModelState.AddModelError(nameof(State.State_ID), "State is used in city master, So state can't be delete..!!");
                    HttpClient htpc = new HttpClient();
                    var lst = WebApiApplication.objCommon.ExecuteDetailByID<State>(htpc, id.ToString(), WebApiApplication.staticVariables.StateApiName);
                    return View(lst);
                }
                var test = WebApiApplication.objCommon.ExecuteDeleteByID(client, id.ToString(), WebApiApplication.staticVariables.StateApiName);
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

        public string GetStateName(int state_id)
        {
            return WebApiApplication.objCommon.GetStateNameByID(state_id);
        }
    }
}
