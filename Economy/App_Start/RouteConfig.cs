using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Economy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("elmah.axd");

            routes.MapMvcAttributeRoutes(); //Attribute routing

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Home", action = "Index", page = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "Invoice",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Invoice", action = "Edit", id = UrlParameter.Optional }
            //);

            //  routes.MapRoute(
            //     name: "Faktura",
            //     url: "faktura/skapa",
            //     defaults: new { controller = "Invoice", action = "Edit", id = UrlParameter.Optional }
            // );

            //  routes.MapRoute(
            //    name: "statistik",
            //    url: "statistik/{action}",
            //    defaults: new { controller = "statistics", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
