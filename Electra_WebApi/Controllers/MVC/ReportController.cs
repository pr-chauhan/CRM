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
            HttpClient client = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Invoice>(client, WebApiApplication.staticVariables.InvoiceApiName);
            return View();
        }
        public ActionResult PartyWiseDateWise()
        {

            HttpClient client = new HttpClient();
            ViewBag.CL = WebApiApplication.objCommon.ExecuteIndex<Consignee>(client, WebApiApplication.staticVariables.ConsigneeApiName);
            return View();
        }
        public ActionResult DatewiseItemwiseSummary()
        {
            return View();
        }
        public ActionResult ItemwiseSummary()
        {
            return View();
        }
    }
}