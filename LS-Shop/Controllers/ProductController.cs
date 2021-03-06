﻿using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using LS_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    public class ProductController : Controller
    {
        #region private members
        private IDbContext dbContext;
        #endregion

        #region public members
        public int PageSize = 4;
        #endregion

        #region constructors
        public ProductController(IDbContext dbContextParam)
        {
            dbContext = dbContextParam;
        }
        #endregion

        #region public methods
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

        public ActionResult List(int? category, int page = 1)
        {
            Category currentCategory;
            if (category == null || category == 0)
            {
                currentCategory = null;                
            }
            else
            {
                currentCategory = dbContext.Categories.Where(o => o.CategoryId == category).First();
            }
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = dbContext.Products.Where(o => category == null || o.CategoryId == category)
                .OrderBy(o => o.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? dbContext.Products.Count() : dbContext.Products.Where(o => o.CategoryId == category).Count()
                },
                CurrentCategory = currentCategory
            };
            return View(model);
        }

        public ActionResult Details(int productId)
        {
            Product product = dbContext.Products.Where(o => o.ProductId == productId).FirstOrDefault();
            return View(product);
        }
        #endregion
    }
}