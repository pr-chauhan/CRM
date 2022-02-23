using EntityClass;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Electra_WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static CommonFunctions objCommon = new CommonFunctions();
        public static StaticVariables staticVariables = new StaticVariables();
        public static CraModel db = new CraModel();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
