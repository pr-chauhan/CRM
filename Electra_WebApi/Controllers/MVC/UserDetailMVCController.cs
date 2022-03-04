using EntityClass;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class UserDetailMVCController : Controller
    {
        private HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                var lst = WebApiApplication.objCommon.ExecuteIndex<User_detail>(client, WebApiApplication.staticVariables.UserDetailApiName);
                return View(lst);
            }
        }

        public ActionResult Details(string id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<User_detail>(client, id.ToString(), WebApiApplication.staticVariables.UserDetailApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User_detail collection)
        {
            try
            {
                client.BaseAddress = null;
                if (WebApiApplication.objCommon.ValidateValue<Invoice>("User_Name", collection.User_Name))
                {
                    ModelState.AddModelError(nameof(User_detail.User_Name), "Duplicate User Name is not allowed..!!");
                    return View(collection);
                }
                var test = WebApiApplication.objCommon.ExecutePost(client, collection, WebApiApplication.staticVariables.UserDetailApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<City>(client, id, WebApiApplication.staticVariables.UserDetailApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Delete(string id, User_detail collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<Invoice>("E_userid", id.ToString()))
                {
                    ModelState.AddModelError(nameof(User_detail.User_Name), "UserName is used in Invoice master, So UserName can't be delete..!!");
                    //return View(collection);
                    HttpClient htpc = new HttpClient();
                    var lst = WebApiApplication.objCommon.ExecuteDetailByID<User_detail>(htpc, id.ToString(), WebApiApplication.staticVariables.UserDetailApiName);
                    return View(lst);
                }
                var test = WebApiApplication.objCommon.ExecuteDeleteByID(client, id.ToString(), WebApiApplication.staticVariables.UserDetailApiName);
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User_detail collection)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<User_detail>(client, collection.User_Name.ToString(), WebApiApplication.staticVariables.UserDetailApiName);
            if (lst != null)
            {
                if (lst.User_Name.Equals(collection.User_Name) && lst.Passwrd.Equals(collection.Passwrd))
                {
                    Session["userName"] = collection.User_Name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "UserDetailMVC");
        }
    }
}