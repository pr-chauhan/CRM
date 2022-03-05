using EntityClass;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class ItemMVCController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
                var lst = WebApiApplication.objCommon.ExecuteIndex<Item>(client, WebApiApplication.staticVariables.ItemApiName);
                return View(lst);
            //}
        }

        public ActionResult Details(int id)
        {
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Item>(client, id.ToString(), WebApiApplication.staticVariables.ItemApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<Item>("Item_Name", collection.Item_Name))
                {
                    ModelState.AddModelError(nameof(Item.Item_Name), "Duplicate item is not allowed..!!");
                    return View(collection);
                }
                var test = WebApiApplication.objCommon.ExecutePost(client, collection, WebApiApplication.staticVariables.ItemApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Item>(client, id.ToString(), WebApiApplication.staticVariables.ItemApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Edit(Item collection)
        {
            try
            {
                var test = WebApiApplication.objCommon.ExecutePut(client, collection, WebApiApplication.staticVariables.ItemApiName);
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
            var lst = WebApiApplication.objCommon.ExecuteDetailByID<Item>(client, id.ToString(), WebApiApplication.staticVariables.ItemApiName);
            return View(lst);
        }

        [HttpPost]
        public ActionResult Delete(int id, Item collection)
        {
            try
            {
                if (WebApiApplication.objCommon.ValidateValue<Invoice_Detail>("Item_id", id.ToString()))
                {
                    ModelState.AddModelError(nameof(Item.Item_ID), "Item is used in Invoice, So item can't be delete..!!");
                    HttpClient htpc = new HttpClient();
                    var lst = WebApiApplication.objCommon.ExecuteDetailByID<Item>(htpc, id.ToString(), WebApiApplication.staticVariables.ItemApiName);
                    return View(lst);
                }
                var test = WebApiApplication.objCommon.ExecuteDeleteByID(client, id.ToString(), WebApiApplication.staticVariables.ItemApiName);
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