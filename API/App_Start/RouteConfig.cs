using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TesteCamposDealer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ClienteCrud",
                url: "cliente/{action}/{id}",
                defaults: new { controller = "ClienteMvc", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProdutoCrud",
                url: "produto/{action}/{id}",
                defaults: new { controller = "ProdutoMvc", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VendaCrud",
                url: "venda/{action}/{id}",
                defaults: new { controller = "VendaMvc", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
