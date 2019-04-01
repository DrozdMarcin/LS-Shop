using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LS_Shop.Data_Access_Layer;
using LS_Shop.Migrations;
using LS_Shop.Models;

namespace LS_Shop.Controllers
{
    public class HomeController : Controller
    {      
        private ProductsContext db = new ProductsContext();
        
        public ActionResult Index()
        {           
            
            var news = db.Products.Where(k => !k.Hidden).OrderByDescending(k => k.DateOfAddition).Take(4).ToList();

            var bestsellers = db.Products.Where(k => !k.Hidden && k.Bestseller).OrderBy(k => Guid.NewGuid()).Take(4).ToList();

            var vm = new HomeViewModel()
            {
                News = news,
                Bestsellers = bestsellers
            };
           
            return View(vm);
        }

        public ActionResult StaticSites(string name)
        {
            return View(name);
        }
    }
}