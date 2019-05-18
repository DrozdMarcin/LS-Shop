using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using LS_Shop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    [Authorize(Roles = "Administrator, Employee")]
    public class AdminController : Controller
    {
        private IDbContext db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this._roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this._roleManager = value; }
        }
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

        public AdminController(IDbContext dbParam)
        {
            db = dbParam;
        }

        public AdminController(IDbContext dbParam, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            db = dbParam;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Categories()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Users()
        {
            var users = UserManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Roles()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles);
        }

        public ActionResult Orders()
        {
            var list = db.Orders.OrderByDescending(o => o.DateOfAddition).ToList();
            return View(list);
        }

        public ActionResult EditOrder(int id)
        {
            var result = new EditOrderViewModel();
            using (var context = new EfDbContext())
            {
                result.Order = context.Orders.Find(id);
                result.OrderPosition = context.OrderPositions.Where(o => o.OrderId == id).Include(o => o.Product).ToList();
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult EditOrder(EditOrderViewModel editedOrder)
        {
            if(ModelState.IsValid)
            {
                using (var context = new EfDbContext())
                {
                    context.Entry(editedOrder.Order).State = EntityState.Modified;
                    context.SaveChanges();
                }
                TempData["message"] = "Udało się zapisać zmiany";
                return RedirectToAction("Orders");
            }
            TempData["message"] = "Nie udało się zapisać zmian";
            return View(editedOrder);
        }

        public ActionResult EditOrderProduct(int id)
        {
            OrderPosition orderPosition;
            using (var context = new EfDbContext())
            {
                orderPosition = context.OrderPositions.Where(o => o.OrderPositionId == id).Include(o => o.Product).FirstOrDefault();
            }
            return View(orderPosition);
        }

        [HttpPost]
        public ActionResult EditOrderProduct(OrderPosition orderPosition)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EfDbContext())
                {
                    if (orderPosition.Amount <= 0)
                    {
                        context.Entry(orderPosition).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Entry(orderPosition).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    orderPosition.Order = context.Orders.Where(o => o.OrderId == orderPosition.OrderId).FirstOrDefault();
                    orderPosition.Order.OrderPosition = context.OrderPositions.Where(o => o.OrderId == orderPosition.OrderId).ToList();
                    orderPosition.Order.OrderValue = 0;
                    foreach (var element in orderPosition.Order.OrderPosition)
                    {
                        orderPosition.Order.OrderValue += element.Amount * element.PurchasePrice;
                    }
                    context.Entry(orderPosition.Order).State = EntityState.Modified;
                    context.SaveChanges();
                    var order = context.Orders.Find(orderPosition.OrderId);
                    if(order.OrderValue <= 0)
                    {
                        context.Entry(order).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                TempData["message"] = "Udało się zapisać zmiany";
                return RedirectToAction("Orders");
            }
            return View(orderPosition);
        }

        public ActionResult DeleteOrder(int id)
        {
            using (var context = new EfDbContext())
            {
                context.Entry(context.Orders.Find(id)).State = EntityState.Deleted;
                context.SaveChanges();
            }
            TempData["message"] = "Udało się usunąć zamówienie";
            return RedirectToAction("Orders");
        }

        public ActionResult DeleteOrderPosition(int id)
        {
            using (var context = new EfDbContext())
            {
                var orderId = context.OrderPositions.Find(id).OrderId;
                context.Entry(context.OrderPositions.Find(id)).State = EntityState.Deleted;
                context.SaveChanges();
                var order = context.Orders.Find(orderId);
                order.OrderPosition = context.OrderPositions.Where(o => o.OrderId == orderId).ToList();
                order.OrderValue = 0;
                foreach (var element in order.OrderPosition)
                {
                    order.OrderValue += element.Amount * element.PurchasePrice;
                }
                if (order.OrderValue <= 0)
                {
                    context.Entry(order).State = EntityState.Deleted;
                }
                else
                {
                    context.Entry(order).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            TempData["message"] = "Udało sie zapisać zmiany";
            return RedirectToAction("Orders");
        }
    }
}