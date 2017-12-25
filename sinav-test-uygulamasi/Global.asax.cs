using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace sinav_test_uygulamasi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<string, sinav_test_uygulamasi.Models.SinavSonuc> Sonuclar =new  Dictionary<string, sinav_test_uygulamasi.Models.SinavSonuc>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
