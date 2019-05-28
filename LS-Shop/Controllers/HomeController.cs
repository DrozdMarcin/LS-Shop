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
        #region public methods
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult StaticSites(string name)
        {
            return View(name);
        }
        #endregion
    }
}