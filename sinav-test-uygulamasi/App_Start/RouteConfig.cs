using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sinav_test_uygulamasi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultMerhaba",
                url: "merhaba",
                defaults: new { controller = "Home", action = "Mrb", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SinaviUygulamaSayfasice",
                url: "sinav/cevapla",
                defaults: new { controller = "Sinav", action = "Cevapla" }
                , constraints: new { sinavadi = @"(?!sorularigetir|cevapla|sinavlistesi).*" }
            );

            routes.MapRoute(
                name: "SinaviUygulamaSayfasi",
                url: "sinav/{sinavadi}",
                defaults: new { controller = "Sinav", action = "Index"}
                ,constraints: new {sinavadi = @"(?!sorularigetir|cevapla|sinavlistesi|sinavdurumu|SoruyaCevapVer).*" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
