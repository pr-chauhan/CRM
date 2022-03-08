using EntityClass;
using System.Net.Http;
using System.Web.Mvc;
using Electra_WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http.Headers;

namespace Electra_WebApi.Controllers
{
    public class InvoiceMVCController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private int itemCount = 0;
        private static string financial_yearr;
        public ActionResult Index()
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
                var lst = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
                lst = lst.Where(x => x.Financial_Yr == financial_yearr).ToList();
                return View(lst);
            //}
        }
        [HttpPost]
        public ActionResult Index(string financial_yr)
        {
            var lst = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
            lst = lst.Where(x => x.Financial_Yr == financial_yr).ToList();
            financial_yearr = financial_yr;
            return RedirectToAction("Index",lst);
        }

        public ActionResult Create()
        {
            HttpClient client1 = new HttpClient();
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client1, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
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
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
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

        public ActionResult Edit(string fyr, int id)
        {
            InvoiceModel invoices = new InvoiceModel();
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
            WebApiApplication.db.SaveChangesAsync();
            invoices.invoice = WebApiApplication.db.Invoices.Find(fyr, id); ;
            WebApiApplication.db.Entry(invoices.invoice).State = System.Data.Entity.EntityState.Detached;
            var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail   Where Invoice_Id=" + id + " and  Financial_Yr ='" + fyr + "'");
            ViewBag.DataList = data;
            ViewBag.IDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.RDT = invoices.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
            ViewBag.ITime = invoices.invoice.Removal_Time;
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            itemCount = 0;
            return View(invoices);
        }
        [HttpPost]
        public ActionResult Edit(InvoiceModel invoiceModel)
        {
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
            ViewBag.GST = WebApiApplication.objCommon.GetGstType();
            client.BaseAddress = new Uri(WebApiApplication.staticVariables.ServerSuffix + "api/" + WebApiApplication.staticVariables.InvoiceApiName);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putdata = client.PutAsJsonAsync(WebApiApplication.staticVariables.InvoiceApiName, invoiceModel.invoice);
            putdata.Wait();
            var test = putdata.Result; //WebApiApplication.objCommon.ExecutePut(client, invoiceModel.invoice.Financial_Yr, invoiceModel.invoice.Invoice_ID, );
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
          
            return View(invoiceModel);

        }

        public void DeleteInvoiceDetail(string fy_year, string invoiceid)
        {
            HttpClient clientDetails = new HttpClient();
            clientDetails.BaseAddress = new Uri(WebApiApplication.staticVariables.ServerSuffix + "api/" + WebApiApplication.staticVariables.Invoice_DetailApiName);
            var responseDet = clientDetails.DeleteAsync(WebApiApplication.staticVariables.Invoice_DetailApiName + "?id=" + invoiceid + "&Financial_Yr=" + fy_year);
            responseDet.Wait();
            var testDet = responseDet.Result;
        }
        public JsonResult UpdateInvoiceDetail(string row, string Item_id, string No_of_pkg, string type, string Qty, string Rate, string Total_amt, string description, string fy_year, string invoiceid)
        {
            if(itemCount == 0)
            { 
                DeleteInvoiceDetail(fy_year, invoiceid);
                itemCount += 1;
            }
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
            return Json(invoice_Detail);
        }

        public ActionResult Details(string fyr, int id)
        {
            InvoiceModel invoices = new InvoiceModel();
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
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
            var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
            var l = WebApiApplication.objCommon.ExecuteIndex<Item>(client2, WebApiApplication.staticVariables.ItemApiName);
            ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
            ViewBag.IT = l.OrderBy(x => x.Item_Name).ToList();
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
        public ActionResult Delete(InvoiceModel invoiceModel)
        {
            //2020-12-31
            //int lRetVal = 0;
            var inv = new Invoice();
            var invDet = new Invoice_Detail();
            inv = WebApiApplication.db.Invoices.Find(invoiceModel.invoice.Financial_Yr, invoiceModel.invoice.Invoice_ID);
            if(inv.Invoice_Date <= System.DateTime.Parse(("2020-12-31")))
            {
                var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail Where Invoice_Id=" + invoiceModel.invoice.Invoice_ID + " and  Financial_Yr =" + invoiceModel.invoice.Financial_Yr);
                ViewBag.DataList = data;
                HttpClient client1 = new HttpClient();
                var lst = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client1, WebApiApplication.staticVariables.ConsigneeApiName);
                ViewBag.CL = lst.OrderBy(x => x.Consignee_Name).ToList();
                ViewBag.IDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.RDT = invoiceModel.invoice.Invoice_Date.Value.ToString("yyyy-MM-dd");
                ViewBag.ITime = invoiceModel.invoice.Removal_Time;
                ViewBag.GST = WebApiApplication.objCommon.GetGstType();
                return View(invoiceModel);
            }

            client.BaseAddress = new Uri(WebApiApplication.staticVariables.ServerSuffix + "api/" + WebApiApplication.staticVariables.InvoiceApiName);
            var response = client.DeleteAsync(WebApiApplication.staticVariables.InvoiceApiName + "?id=" + invoiceModel.invoice.Invoice_ID + "&Financial_Yr=" + invoiceModel.invoice.Financial_Yr);
            response.Wait();
            var test = response.Result;

            HttpClient clientDetails = new HttpClient();
            clientDetails.BaseAddress = new Uri(WebApiApplication.staticVariables.ServerSuffix + "api/" + WebApiApplication.staticVariables.Invoice_DetailApiName);
            var responseDet = clientDetails.DeleteAsync(WebApiApplication.staticVariables.Invoice_DetailApiName + "?id=" + invoiceModel.invoice.Invoice_ID + "&Financial_Yr=" + invoiceModel.invoice.Financial_Yr);
            responseDet.Wait();
            var testDet = responseDet.Result;
            if (test.IsSuccessStatusCode && testDet.IsSuccessStatusCode)
                {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public string GetInvoiceNo(string financial_yr)
        {
            var InvoiceNo = WebApiApplication.objCommon.GetInvoiceMaxNo(financial_yr);
            return InvoiceNo;
        }

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
               if(lRetVal > 0)
                {
                    var data = WebApiApplication.db.Invoice_Detail.SqlQuery("Select * from Invoice_Detail Where Invoice_Id=" + id + " and  Financial_Yr ='" + financial_yr + "'");
                    foreach(var dt in data)
                    {
                       invDet =  WebApiApplication.db.Invoice_Detail.Find(dt.ID);
                        WebApiApplication.db.Invoice_Detail.Remove(invDet);
                        WebApiApplication.db.SaveChangesAsync();

                    }
                }
            }
            return lRetVal;
        }
    }
}