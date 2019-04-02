using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    public class CategoryController : Controller
    {
        private IDbContext dbContext;

        public CategoryController(IDbContext dbContextParam)
        {
            dbContext = dbContextParam;
        }

        public ActionResult List(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<Category> categories = dbContext.Categories;
            return PartialView(categories);
        }
    }
}