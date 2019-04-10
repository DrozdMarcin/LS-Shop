﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LS_Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductsList",
                url: "produkty/{*catchall}",
                defaults: new { controller = "Product", action = "List"}
                );      

            routes.MapRoute(
                name: "Search",
                url: "szukaj",
                defaults: new { controller = "Search", action = "Search"}
                );

            routes.MapRoute(
                name: "Cart",
                url: "koszyk",
                defaults: new { controller = "Cart", action = "Cart" }
                );

            routes.MapRoute(
                name: "Register",
                url: "rejestracja",
                defaults: new { controller = "Account", action = "Register" }
                );

            routes.MapRoute(
                name: "Login",
                url: "logowanie",
                defaults: new { controller = "Account", action = "Login" }
                );

            routes.MapRoute(
                name: "StaticSites",
                url: "{name}.html",
                defaults: new { controller = "Home", action = "StaticSites"} );

            routes.MapRoute(
                name: "Index",
                url: "index",
                defaults: new { controller = "Home", action = "Index"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
