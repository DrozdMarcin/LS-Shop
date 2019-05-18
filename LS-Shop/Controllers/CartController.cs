using LS_Shop.Data_Access_Layer;
using LS_Shop.Infrastructure;
using LS_Shop.Models;
using LS_Shop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            if (cart.Count == 0 || cart == null)
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
            var cart = cartManager.GetCart();
            var product = cart.Find(o => o.Product.ProductId == productId);
            TempData["Message"] = string.Format("Dodano do koszyka: {0}", product.Product.Name);
            return RedirectToAction("Cart");
        }

        public ActionResult DeleteFromCart(int productId)
        {
            cartManager.DeleteFromCart(productId);
            TempData["Message"] = "Usunięto produkt z koszyka";
            return RedirectToAction("Cart");
        }

        public ActionResult OneDeleteFromCart(int productId)
        {
            cartManager.OneDeleteFromCart(productId);
            TempData["Message"] = "Usunięto produkt z koszyka";
            return RedirectToAction("Cart");
        }

        [Authorize]
        public ActionResult CreateOrder()
        {
            return View();
        }

        public int GetCartQuantity()
        {
            return cartManager.GetCartQuantity();
        }

        public async Task<ActionResult> PayOrder()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var order = new Order
                {
                    Email = user.UserName,
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Street = user.UserData.Street,
                    City = user.UserData.City,
                    PostalCode = user.UserData.PostalCode,
                    PhoneNumber = user.UserData.PhoneNumber
                  
                };
                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnurl = Url.Action("PayOrder", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> PayOrder (Order orderDetails)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = cartManager.CreateOrder(orderDetails, userId);
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);
                cartManager.ClearCart();
                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }

        public ActionResult OrderConfirmation()
        {
            return View();
        }


        public ActionResult History()
        {
            var userEmail = User.Identity.GetUserName();
            List<Order> orderList = new List<Order>();
            using (var context = new EfDbContext())
            {
                orderList = context.Orders.Where(o => o.Email == userEmail).Include(o => o.OrderPosition).OrderByDescending(o => o.DateOfAddition).ToList();
            }             
            return View(orderList);
        }

        public ActionResult OrderDetails(int id)
        {
            OrderDetailsViewModel order = new OrderDetailsViewModel();
            using (var context = new EfDbContext())
            {
                order.Order = context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
                order.OrderPositions = context.OrderPositions.Where(o => o.OrderId == order.Order.OrderId).Include(o => o.Product).ToList();
            }  
            
            return View(order);
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            }
            private set
            {
                _userManager = value;
            }

        }
    }
}