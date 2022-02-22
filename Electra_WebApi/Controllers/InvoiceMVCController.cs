using EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Electra_WebApi.Controllers
{
    public class InvoiceMVCController : Controller
    {
        HttpClient client = new HttpClient();
        private CraModel db = new CraModel();
        // GET: InvoiceMVC
        public ActionResult Index()
        {
            List<Invoice> list = new List<Invoice>();
            client.BaseAddress = new Uri("https://localhost:44305/api/InvoiceApi");
            var response = client.GetAsync("InvoiceApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Invoice>>();
                display.Wait();
                list = display.Result;
            }
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.CL = GetConsigneeList();
            ViewBag.IT = GetItemeList();
            ViewBag.GST = GetGstType();
            return View();
        }

        public List<Consignee> GetConsigneeList()
        {
            List<Consignee> list = new List<Consignee>();
            HttpClient clientc = new HttpClient();
            clientc.BaseAddress = new Uri("https://localhost:44305/api/ConsigneeApi");
            var response = clientc.GetAsync("ConsigneeApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Consignee>>();
                display.Wait();
                list = display.Result;
            }
            return list;
        }
        public List<Item> GetItemeList()
        {
            List<Item> list = new List<Item>();
            HttpClient clientt = new HttpClient();
            clientt.BaseAddress = new Uri("https://localhost:44305/api/ItemApi");
            var response = clientt.GetAsync("ItemApi");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Item>>();
                display.Wait();
                list = display.Result;
            }
            return list;
        }

        public List<SelectListItem> GetGstType()
        {
            var Gsttypes = new List<SelectListItem>();
            Gsttypes.Add(new SelectListItem() { Value = string.Empty , Text = "Select..."});
            Gsttypes.Add(new SelectListItem() { Value = "GST",Text = "GST" });
            Gsttypes.Add(new SelectListItem() { Value = "CGST",Text = "CGST" });
            Gsttypes.Add(new SelectListItem() { Value = "SGST", Text = "SGST" });

            return Gsttypes;
        }
        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceModel)
        {
            ViewBag.CL = GetConsigneeList();
            ViewBag.IT = GetItemeList();
            ViewBag.GST = GetGstType();
            client.BaseAddress = new Uri("https://localhost:44305/api/InvoiceApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putdata = client.PostAsJsonAsync("InvoiceApi", invoiceModel.invoice);
            putdata.Wait();

            var test = putdata.Result;
            if (test.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");

            }
            //return View(invoiceModel.invoice);
            return View();

        }

        public JsonResult SaveInvoiceDetail(string Item_id, string No_of_pkg, string Qty, string Rate, string Total_amt)
        {
           Invoice_Detail invoice_Detail = new Invoice_Detail();
            invoice_Detail.Item_id =int.Parse(Item_id);
            invoice_Detail.No_of_pkg = int.Parse(No_of_pkg);
            invoice_Detail.Qty = int.Parse( Qty);
            invoice_Detail.Rate = int.Parse(Rate);
            invoice_Detail.Total_amt = int.Parse(Total_amt);
            HttpClient clientDetails = new HttpClient();
            clientDetails.BaseAddress = new Uri("https://localhost:44305/api/Invoice_DetailApi");
            clientDetails.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putdatadet = clientDetails.PostAsJsonAsync("Invoice_DetailApi", invoice_Detail);
            putdatadet.Wait();

            var testdet = putdatadet.Result;
            if (testdet.IsSuccessStatusCode)
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
            //invoices = db.Invoices.Where(x => x.Financial_Yr.Equals(fyr) && x.Invoice_ID.Equals(id)).ToList();

            ViewBag.CL = GetConsigneeList();
            ViewBag.IT = GetItemeList();
            ViewBag.GST = GetGstType();
            return View(invoices);
        }
    }
}