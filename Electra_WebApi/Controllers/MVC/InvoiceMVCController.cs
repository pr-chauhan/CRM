using EntityClass;
using System.Net.Http;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class InvoiceMVCController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
            return View(lst);
        }

        public ActionResult Create()
        {
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View();
        }

        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            var test = WebApiApplication.objCommon.ExecutePost(client, invoiceModel.invoice, WebApiApplication.staticVariables.InvoiceApiName);
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public JsonResult SaveInvoiceDetail(string Item_id, string No_of_pkg, string Qty, string Rate, string Total_amt)
        {
            Invoice_Detail invoice_Detail = new Invoice_Detail
            {
                Item_id = int.Parse(Item_id),
                No_of_pkg = int.Parse(No_of_pkg),
                Qty = int.Parse(Qty),
                Rate = int.Parse(Rate),
                Total_amt = int.Parse(Total_amt)
            };
            HttpClient clientDetails = new HttpClient();
            var test = WebApiApplication.objCommon.ExecutePost(clientDetails, invoice_Detail, WebApiApplication.staticVariables.Invoice_DetailApiName);
            if (test.IsSuccessStatusCode)
            {
                
            }
            else
            {
                 
            }
            return Json(invoice_Detail);
        }

        public ActionResult Edit()
        {
            InvoiceModel invoices = new InvoiceModel();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View(invoices);
        }
    }
}