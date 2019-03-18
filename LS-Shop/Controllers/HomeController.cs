using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;

namespace LS_Shop.Controllers
{
    public class HomeController : Controller
    {
        private ProductsContext db = new ProductsContext();

        public ActionResult Index()
        {
            Product czekolada = new Product
            {
                Name = "Czekolada gorzka 85%",
                NameOfImage = "czekolada.png",
                Description = "Wyśmienita czekolada ciemna 85%"
            };

            Category slodycze = new Category
            {
                Name = "Słodycze",
                NameOfImage = "slodycze.png",
                Description = "Słodycze najwyższej jakości spożywczej z ekologicznych składników"
            };


            db.Categories.Add(slodycze);
            //db.Products.Add(czekolada);
            db.SaveChanges();
            return View();
        }
    }
}