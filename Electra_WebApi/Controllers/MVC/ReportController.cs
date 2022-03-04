using System.Net.Http;
using System.Web.Mvc;
using EntityClass;
namespace Electra_WebApi.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                HttpClient client = new HttpClient();
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
                return View();
            }
        }
        public ActionResult PartyWiseDateWise()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                HttpClient client = new HttpClient();
                ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
                return View();
            }
        }
        public ActionResult DatewiseItemwiseSummary(string fromdate, string todate)
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                if (fromdate == null || todate == null)
                {
                    return View();
                }
                else
                {

                    return null;
                }
            }
        }
       
        public RedirectResult OpenReport(string fromdate, string todate)
        {
            return Redirect("/Reports/MonthlySummary.aspx");
        }

        public ActionResult ItemwiseSummary()
        {
            if (Session["userName"] == null)
            {
                return RedirectToAction("Login", "UserDetailMVC");
            }
            else
            {
                return View();
            }
        }
    }
}