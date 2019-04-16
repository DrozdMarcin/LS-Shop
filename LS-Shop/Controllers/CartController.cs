using LS_Shop.Infrastructure;
using LS_Shop.Models;
using LS_Shop.ViewModels;
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
            List<PositionCart> cart = cartManager.GetCart();
            if(cart.Count == 0 || cart == null)
            {
                ViewBag.ErrorMessage = "Twój koszyk jest pusty.";
                return View("Error");
            }
            decimal totalValue = cartManager.GetCartValue();
            CartViewModel cartVM = new CartViewModel()
            {
                PositionCart = cart,
                TotalValue = totalValue
            };
            return View(cartVM);
        }

        public ActionResult AddToCart(int productId)
        {
            cartManager.AddToCart(productId);
            return RedirectToAction("Cart");
        }

        public ActionResult DeleteFromCart(int productId)
        {
            cartManager.DeleteFromCart(productId);
            return RedirectToAction("Cart");
        }

        public int GetCartQuantity()
        {
            return cartManager.GetCartQuantity();
        }
    }
}