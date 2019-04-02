using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    public class ProductController : Controller
    {
        private IDbContext dbContext;

        public ProductController(IDbContext dbContextParam)
        {
            dbContext = dbContextParam;
        }

        
        public ActionResult NewProducts()
        {
            var products = dbContext.Products.Where(k => !k.Hidden).OrderByDescending(k => k.DateOfAddition).Take(4).ToList();
            var viewModel = new NewProductsViewModel()
            {
                Products = products
            };
            return PartialView(viewModel);
        }

        public ActionResult Bestsellers()
        {
            var bestsellers = dbContext.Products.Where(k => !k.Hidden && k.Bestseller).OrderBy(k => Guid.NewGuid()).Take(4).ToList();
            var viewModel = new BestsellersViewModel()
            {
                Products = bestsellers
            };
            return PartialView(viewModel);
        }
    }
}