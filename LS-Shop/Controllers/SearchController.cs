using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using LS_Shop.ViewModels;

namespace LS_Shop.Controllers
{
    public class SearchController : Controller
    {
        private IDbContext dbContext;

        public SearchController(IDbContext dbContextParam)
        {
            dbContext = dbContextParam;
        }

        // GET: Search
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string id)
        {
            if(id == null || id.Equals(""))
            {
                return View();
            }
            SearchViewModel searchViewModel = new SearchViewModel();

            searchViewModel.SearchText = id;
            searchViewModel.Products = dbContext.Products.Where(product => product.Name.ToLower().Contains(id.ToLower()));

            if (searchViewModel.Products.Any())
                searchViewModel.AnySearchingProductExists = true;
            else searchViewModel.AnySearchingProductExists = false;

            return View(searchViewModel);
        }
    }
}