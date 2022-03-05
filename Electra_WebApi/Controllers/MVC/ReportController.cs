using System.Net.Http;
using System.Web.Mvc;
using EntityClass;
namespace Electra_WebApi.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report


    
         
        public ActionResult Index(string Financial_Yr, string Invoice_ID, string optoin)
        {

            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{

                if (Financial_Yr == null || Invoice_ID == null)
                {
                    HttpClient client = new HttpClient();
                    ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
                    return View();
                }
                else
                {

                    StaticVariables.Financial_Year = Financial_Yr;
                    StaticVariables.Invoice_No = Invoice_ID;
                    StaticVariables.Option = optoin;
                    return Redirect(Url.Content("~/Reports/PrintInvoice.aspx"));

                    //return null;
                }
            //}
        }
        public ActionResult PartyWiseDateWise(string consignee_id, string fromdate, string todate)
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
                if (fromdate == null || todate == null)
                {

                    HttpClient client = new HttpClient();
                    ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
                    return View();
                }
                else
                {
                    StaticVariables.From_Date = fromdate;
                    StaticVariables.To_Date = todate;
                    StaticVariables.Consignee_ID = consignee_id;
                    return Redirect(Url.Content("~/Reports/DataWiseInvoiceList.aspx"));

                }
            //}
        }
        public ActionResult DatewiseItemwiseSummary(string fromdate, string todate)
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
                if (fromdate == null || todate == null)
                {
                    return View();
                }
                else
                {
                    
                    StaticVariables.From_Date = fromdate;
                    StaticVariables.To_Date = todate;
                    return Redirect(Url.Content("~/Reports/MonthlySummary.aspx"));
                   
                    //return null;
                }
            //}
        }
       
       

        public ActionResult ItemwiseSummary(string fromdate, string todate)
        {
            //if (StaticVariables.UserName == null)
            //{
            //    return RedirectToAction("Login", "UserDetailMVC");
            //}
            //else
            //{
                if (fromdate == null || todate == null)
                {
                    return View();
                }
                else
                {

                    StaticVariables.From_Date = fromdate;
                    StaticVariables.To_Date = todate;
                    return Redirect(Url.Content("~/Reports/ItemwiseSummary.aspx"));

                    //return null;
                }
            //}
        }
    }
}