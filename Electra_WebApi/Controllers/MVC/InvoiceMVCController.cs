using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using Electra_WebApi.Models;
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
            HttpClient client1 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client1, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            //ViewBag.INVID = WebApiApplication.objCommon.GetInvoiceMaxNo();
            ViewBag.IDT = System.DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.ITime = System.DateTime.Now.ToString("hh:mm:ss");
            return View();
        }

        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
          
            var test = WebApiApplication.objCommon.ExecutePost(client, invoiceModel.invoice, WebApiApplication.staticVariables.InvoiceApiName);
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public JsonResult SaveInvoiceDetail(string Item_id, string No_of_pkg,string type, string Qty, string Rate, string Total_amt,string description,string fy_year,string invoiceid)
        {
            Invoice_Detail invoice_Detail = new Invoice_Detail
            {
                Financial_Yr = fy_year,
                Invoice_Id = int.Parse(invoiceid),
                Item_id = int.Parse(Item_id),
                TYPE = type,
                No_of_pkg = int.Parse(No_of_pkg),
                Qty = int.Parse(Qty),
                Rate = int.Parse(Rate),
                Total_amt = float.Parse(Total_amt),
                DEC = description
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
            Invoice invoices = new Invoice();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View(invoices);
        }

        public string GetInvoiceNo(string financial_yr)
        {
            var InvoiceNo = WebApiApplication.objCommon.GetInvoiceMaxNo(financial_yr);
            return InvoiceNo;
        }

        public bool DeletePerformaInvoice(string financial_yr, string id)
        {
            bool lRetVal = true;
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            var res= WebApiApplication.objCommon.ExecuteDeleteByID(client1, id ,WebApiApplication.staticVariables.InvoiceApiName);
            if(res.IsSuccessStatusCode)
            { 
                var res1 = WebApiApplication.objCommon.ExecuteDeleteByID(client2, id, WebApiApplication.staticVariables.Invoice_DetailApiName);
                if(!res1.IsSuccessStatusCode)
                {
                    lRetVal = false;
                }
            }
            else
            {
                lRetVal = false;
            }
            return lRetVal;
        }
    }
}