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
        #region private members
        private IDbContext dbContext;
        #endregion

        #region constructors
        public CategoryController(IDbContext dbContextParam)
        {
            dbContext = dbContextParam;
        }
        #endregion

        #region public methods
        public ActionResult List(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<Category> categories = dbContext.Categories.Where(o => o.Hidden != true).ToList();
            return PartialView(categories);
        }
        #endregion
    }
}