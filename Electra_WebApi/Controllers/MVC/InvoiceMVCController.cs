using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using Electra_WebApi.Models;
using System.Collections.Generic;
using System.Linq;

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
            ViewBag.IDT = System.DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.ITime = System.DateTime.Now.ToString("hh:mm:ss");
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

        public ActionResult Edit(string fyr, int id)
        {
            InvoiceModel invoices = new InvoiceModel();
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            invoices.invoice = WebApiApplication.db.Invoices.Find(fyr, id);
            var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail   Where Invoice_Id=" + id + " and  Financial_Yr ='" + fyr + "'");
            ViewBag.DataList = data;
            ViewBag.IDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.RDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.ITime = invoices.invoice.Removal_Time;
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View(invoices);
        }
        [HttpPost]
        public ActionResult Edit(InvoiceModel invoiceModel)
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
            else
            {
                var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail Where Invoice_Id=" + invoiceModel.invoice.Invoice_ID + " and  Financial_Yr ='" + invoiceModel.invoice.Financial_Yr + "'");
                ViewBag.DataList = data;
                ViewBag.IDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.RDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.ITime = invoiceModel.invoice.Removal_Time;
                return View(invoiceModel);
            }
           
        }
        [HttpPost]
        public JsonResult UpdateInvoiceDetail(int id,string Item_id, string No_of_pkg, string type, string Qty, string Rate, string Total_amt, string description, string fy_year, string invoiceid)
        {
            Invoice_Detail invoice_Detail = new Invoice_Detail
            {
                Financial_Yr = fy_year,
                Invoice_Id = int.Parse(invoiceid),
                Item_id = int.Parse(Item_id),
                TYPE = type,
                No_of_pkg = int.Parse(No_of_pkg),
                Qty = decimal.Parse(Qty),
                Rate = decimal.Parse(Rate),
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

        public ActionResult Details(string fyr, int id)
        {
            InvoiceModel invoices = new InvoiceModel();
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            invoices.invoice = WebApiApplication.db.Invoices.Find(fyr, id);
            var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail   Where Invoice_Id=" + id + " and  Financial_Yr ='" + fyr + "'");
            ViewBag.DataList = data;
            ViewBag.IDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.RDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.ITime = invoices.invoice.Removal_Time;
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View(invoices);
        }

        public ActionResult Delete(string fyr, int id)
        {
            InvoiceModel invoices = new InvoiceModel();
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            ViewBag.IT = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            invoices.invoice = WebApiApplication.db.Invoices.Find(fyr, id);
            var data = WebApiApplication.db.Invoice_Detail.Where(x => x.Invoice_Id == id && x.Financial_Yr == fyr).ToList();
            ViewBag.DataList = data;
            ViewBag.IDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.RDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.ITime = invoices.invoice.Removal_Time;
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            return View(invoices);
        }
        [HttpPost]
        public ActionResult Delete(InvoiceModel invoiceModel)
        {
            //2020-12-31
            int lRetVal = 0;
            var inv = new Invoice();
            var invDet = new Invoice_Detail();
            inv = WebApiApplication.db.Invoices.Find(invoiceModel.invoice.Financial_Yr, invoiceModel.invoice.Invoice_ID);
            if( inv.Invoice_Date.Value <= System.DateTime.Parse(("2020-12-31")))
            {
                var data = WebApiApplication.db.Invoice_Detail.Where(x => x.Invoice_Id == invoiceModel.invoice.Invoice_ID && x.Financial_Yr == invoiceModel.invoice.Financial_Yr).ToList();
                ViewBag.DataList = data;
                HttpClient client1 = new HttpClient();
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
                ViewBag.IDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.RDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.ITime = invoiceModel.invoice.Removal_Time;
                ViewBag.GST = WebApiApplication.objCommon.GetGstType();
                return View(invoiceModel);
            }
            if (inv != null)
            {
                WebApiApplication.db.Invoices.Remove(inv);
                WebApiApplication.db.SaveChangesAsync();
                lRetVal = 1;
                if (lRetVal > 0)
                {
                    List<Invoice_Detail> data = WebApiApplication.db.Invoice_Detail.Where(x=> x.Invoice_Id == invoiceModel.invoice.Invoice_ID  && x.Financial_Yr == invoiceModel.invoice.Financial_Yr).ToList();
                    if(data.Count>0)
                    {
                        WebApiApplication.db.Invoice_Detail.RemoveRange(data);
                        WebApiApplication.db.SaveChangesAsync();

                    }
                }
            }
            return RedirectToAction("Index");
        }

        public string GetInvoiceNo(string financial_yr)
        {
            var InvoiceNo = WebApiApplication.objCommon.GetInvoiceMaxNo(financial_yr);
            return InvoiceNo;
        }
        [HttpPost]
        public int DeletePerformaInvoice(string financial_yr, int id)
        {
            int lRetVal = 0;
            var inv = new Invoice();
            var invDet = new Invoice_Detail();
            inv = WebApiApplication.db.Invoices.Find( financial_yr , id );
            if (inv != null)
            {
                WebApiApplication.db.Invoices.Remove(inv);
                WebApiApplication.db.SaveChangesAsync();
                lRetVal = 1;
               if(lRetVal > 0)
                {
                    List<Invoice_Detail> data = WebApiApplication.db.Invoice_Detail.Where(x => x.Invoice_Id == id && x.Financial_Yr == financial_yr).ToList();
                    if (data.Count > 0)
                    {
                        WebApiApplication.db.Invoice_Detail.RemoveRange(data);
                        WebApiApplication.db.SaveChangesAsync();
                        
                    }
                }
            }
            return lRetVal;
        }
    }
}