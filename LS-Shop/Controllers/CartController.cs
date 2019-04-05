using LS_Shop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    public class CartController : Controller
    {
        private ISessionManager sessionManager;
        private ICartManager cartManager;

        public CartController(ISessionManager sessionManagerParam, ICartManager cartManagerParam)
        {
            sessionManager = sessionManagerParam;
            cartManager = cartManagerParam;
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult AddIntoCart(string id)
        {
            return View();
        }
    }
}