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

        public ActionResult AddUser()
        {
            var newUser = new AddUserViewModel();
            var roles = RoleManager.Roles.ToList();
            newUser.User = new ApplicationUser();
            newUser.Roles = RoleManager.Roles.ToList().Select(o => new SelectListItem
            {
                Selected = roles.Contains(o),
                Text = o.Name,
                Value = o.Name
            }).OrderBy(o => o.Text);
            return View(newUser);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.User.Email, Email = model.User.Email, UserData = new UserData() };
                var result = await UserManager.CreateAsync(user, model.Password);
                var roleId = RoleManager.FindByName(model.UserRole).Id;
                var roleResult = UserManager.AddToRole(user.Id, RoleManager.FindById(roleId).Name);
                TempData["message"] = "Udało się dodać użytkownika";
                return RedirectToAction("Users");
            }
            else
            {
                TempData["message"] = "Nie udało się dodać użytkownika";
                return View(model);
            }
        }

        public ActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            UserManager.Delete(user);
            TempData["message"] = "Udało się usunąć użytkownika";
            return RedirectToAction("Users");
        }

        public ActionResult UsersInRole(string id)
        {
            var role = RoleManager.FindById(id);
            var usersInRole = role.Users.Where(o => o.RoleId == role.Id).ToList();
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach(var user in usersInRole)
            {
                list.Add(UserManager.FindById(user.UserId));
            }
            var usersViewModel = new UsersInRoleViewModel();
            usersViewModel.Role = role;
            usersViewModel.Users = list;
            return View(usersViewModel);
        }

        public ActionResult AddUserToRole(string roleId)
        {
            var role = RoleManager.FindById(roleId);
            var users = UserManager.Users.ToList();
            var usersInRole = role.Users.Where(o => o.RoleId == role.Id).ToList();
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach(var user in users)
            {
                if(!UserManager.IsInRole(user.Id, role.Id))
                {
                    list.Add(UserManager.FindById(user.Id));
                }
            }
            var usersViewModel = new UsersInRoleViewModel();
            usersViewModel.Role = role;
            usersViewModel.Users = list;
            return View(usersViewModel);
        }

        public ActionResult AddUserToRoleB(string userId, string roleId)
        {
            UserManager.AddToRole(userId, RoleManager.FindById(roleId).Name);
            TempData["message"] = "Udało się dodać użytkownika do roli";
            return RedirectToAction("Roles");
        }

        public ActionResult DeleteUserFromRole(string userId, string roleId)
        {
            UserManager.RemoveFromRole(userId, RoleManager.FindById(roleId).Name);
            TempData["message"] = "Udało się usunąć użytkownika z roli";
            return RedirectToAction("Roles");
        }
    }
}