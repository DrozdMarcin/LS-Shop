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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Controllers
{
    [Authorize(Roles = "Administrator, Employee")]
    public class AdminController : Controller
    {
        #region private members
        private IDbContext db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        #endregion

        #region properties
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
        #endregion

        #region constructors
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
        #endregion

        #region public methods
        // GET: Admin
        public ActionResult Index()
        {
            int? quantityLimit = db.Settings.FirstOrDefault().QuantityOfProductsLimit;
            AdminIndexViewModel viewModel = new AdminIndexViewModel();
            if (quantityLimit > 0)
            {
                var productsWithLowQuantity = db.Products.Where(o => o.Quantity <= quantityLimit).ToList();
                viewModel.ProductsWithLowQuantity = productsWithLowQuantity;
            }
            else
            {
                viewModel.ProductsWithLowQuantity = new List<Product>();
            }
            var latestOrders = db.Orders.Where(o => o.OrderStatus == OrderStatus.Nowy).OrderByDescending(o => o.DateOfAddition).ToList();
            if(latestOrders.Count() > 0)
            {
                viewModel.LatestOrders = latestOrders;
            }
            else
            {
                viewModel.LatestOrders = new List<Order>();
            }
            return View(viewModel);
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
            var adminRoleId = RoleManager.Roles.FirstOrDefault(o => o.Name == "Administrator").Id;
            var viewModelList = new List<UsersViewModel>();
            foreach (var user in users)
            {
                if (user.Roles.Where(o => o.RoleId == adminRoleId && o.UserId == user.Id).Any())
                {
                    viewModelList.Add(new UsersViewModel
                    {
                        User = user,
                        IsAdministrator = true
                    });
                }
                else
                {
                    viewModelList.Add(new UsersViewModel
                    {
                        User = user,
                        IsAdministrator = false
                    });
                }
            }
            return View(viewModelList);
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
                result.OrderPosition = context.OrderPositions.Where(o => o.OrderId == id).ToList();
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult EditOrder(EditOrderViewModel editedOrder)
        {
            if (ModelState.IsValid)
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
            OrderPosition orderPosition = db.OrderPositions.FirstOrDefault(o => o.OrderPositionId == id);
            var model = new EditOrderProductViewModel()
            {
                OrderPositionId = id,
                OrderId = orderPosition.OrderId,
                ProductName = orderPosition.ProductName,
                Quantity = 0
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrderProduct(EditOrderProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Quantity > 0)
                {
                    using (var context = new EfDbContext())
                    {
                        var orderPosition = context.OrderPositions.Find(viewModel.OrderPositionId);
                        if (viewModel.Quantity <= 0)
                        {
                            context.Entry(orderPosition).State = EntityState.Deleted;
                            context.SaveChanges();
                            TempData["message"] = "Udało się usunąć pozycję z zamówienia";
                            return RedirectToAction("Orders");
                        }
                        var orderPositions = context.OrderPositions.Where(o => o.OrderId == viewModel.OrderId).ToList();
                        var product = context.Products.FirstOrDefault(o => o.Name == viewModel.ProductName);
                        product.Quantity += orderPosition.Amount;
                        if (viewModel.Quantity > product.Quantity)
                        {
                            TempData["message"] = "Nie ma aż tyle tego produktu w magazynie!";
                            return View(viewModel);
                        }
                        orderPosition.Amount = viewModel.Quantity;
                        product.Quantity -= viewModel.Quantity;
                        context.Entry(product).State = EntityState.Modified;
                        context.Entry(orderPosition).State = EntityState.Modified;
                        context.SaveChanges();
                        var order = context.Orders.Find(viewModel.OrderId);
                        order.OrderValue = 0;
                        foreach (var element in order.OrderPosition)
                        {
                            order.OrderValue += element.Amount * element.PurchasePrice;
                        }
                        context.Entry(order).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    TempData["message"] = "Udało się zapisać zmiany";
                    return RedirectToAction("Orders");
                }
                else
                {
                    TempData["message"] = "Ilość produktu w zamówieniu nie może być mniejsza od 0.";
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        public ActionResult DeleteOrderPosition(int id)
        {
            using (var context = new EfDbContext())
            {
                var orderId = context.OrderPositions.Find(id).OrderId;
                var orderPosition = context.OrderPositions.Find(id);
                var product = context.Products.Where(o => o.Name == orderPosition.ProductName).FirstOrDefault();
                product.Quantity += orderPosition.Amount;
                context.SaveChanges();
                var order = context.Orders.Find(orderId);
                var orderPositions = context.OrderPositions.Where(o => o.OrderId == orderId).ToList();
                if (orderPositions.Count() > 1)
                {
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
                }
                else
                {
                    order.OrderStatus = OrderStatus.Anulowano;
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
            newUser.Roles = RoleManager.Roles.ToList();
            return View(newUser);
        }

        public ActionResult AddProduct(int? id)
        {
            AddProductViewModel productVM = new AddProductViewModel();
            Product product;
            if (id.HasValue)
            {
                product = db.Products.FirstOrDefault(o => o.ProductId == id);
            }
            else
            {
                product = new Product();
            }
            productVM.Categories = db.Categories.ToList();
            productVM.Product = product;
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddProductViewModel model)
        {
            if (model.NewFile != null && model.NewFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.NewFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/Products/"), fileName);
                model.NewFile.SaveAs(path);
                model.Product.NameOfImage = fileName;
            }

            if (model.Product.ProductId > 0)
            {
                using (var context = new EfDbContext())
                {
                    context.Entry(model.Product).State = EntityState.Modified;
                    context.SaveChanges();
                }
                TempData["message"] = "Udało się zaktualizować produkt";
                return RedirectToAction("Products");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.Product.DateOfAddition = DateTime.Now;
                    using (var context = new EfDbContext())
                    {
                        context.Entry(model.Product).State = EntityState.Added;
                        context.SaveChanges();
                    }
                    TempData["message"] = "Udało się dodać produkt";
                    return RedirectToAction("Products");
                }
                else
                {
                    model.Categories = db.Categories.ToList();
                    return View(model);
                }
            }
        }

        public ActionResult AddCategory(int? id)
        {
            Category category = new Category();
            if (id.HasValue)
            {
                category = db.Categories.FirstOrDefault(o => o.CategoryId == id);
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category)
        {


            if (category.CategoryId > 0)
            {
                using (var context = new EfDbContext())
                {
                    context.Entry(category).State = EntityState.Modified;
                    context.SaveChanges();
                }
                TempData["message"] = "Udało się zaktualizować kategorię";
                return RedirectToAction("Categories");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    using (var context = new EfDbContext())
                    {
                        context.Entry(category).State = EntityState.Added;
                        context.SaveChanges();
                    }
                    TempData["message"] = "Udało się dodać kategorię";
                    return RedirectToAction("Categories");
                }
                else
                {

                    return View(category);
                }
            }
        }

        [HttpPost]
        public ActionResult AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.User.Email, Email = model.User.Email, UserData = new UserData() };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded == false)
                {
                    TempData["message"] = "Hasło powinno się składać z minimum 8 znaków, jednej wielkiej litery, jednej cyfry i jednego znaku specjalnego.";
                    model.Roles = RoleManager.Roles.ToList();
                    return View(model);
                }
                var roleResult = UserManager.AddToRole(user.Id, RoleManager.FindById(model.UserRoleId).Name);
                TempData["message"] = "Udało się dodać użytkownika";
                return RedirectToAction("Users");
            }
            else
            {
                model.Roles = RoleManager.Roles.ToList();
                TempData["message"] = "Nie udało się dodać użytkownika";
                return View(model);
            }
        }

        public ActionResult DeleteProduct(string id)
        {
            var product = db.Products.First(f => f.ProductId == int.Parse(id));
            db.Delete(product);
            TempData["message"] = "Udało się usunąć produkt";
            return RedirectToAction("Products");
        }

        public ActionResult DeleteCategory(string id)
        {
            var category = db.Categories.First(f => f.CategoryId == int.Parse(id));
            db.Delete(category);
            TempData["message"] = "Udało się usunąć kategorię";
            return RedirectToAction("Categories");
        }

        public ActionResult UsersInRole(string id)
        {
            var role = RoleManager.FindById(id);
            var usersInRole = role.Users.Where(o => o.RoleId == role.Id).ToList();
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach (var user in usersInRole)
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
            foreach (var user in users)
            {
                if (!UserManager.IsInRole(user.Id, role.Id))
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

        public ActionResult Delivery(int id)
        {
            var viewModel = new DeliveryViewModel();
            viewModel.ProductId = id;
            viewModel.DeliveredQuantity = 0;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delivery(DeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DeliveredQuantity <= 0)
                {
                    TempData["message"] = "Ilość dostarczonych produktów musi być większa od 0.";
                    return View(model);
                }
                using (var context = new EfDbContext())
                {
                    var product = context.Products.Find(model.ProductId);
                    product.Quantity += model.DeliveredQuantity;
                    context.SaveChanges();
                }
                TempData["message"] = "Udało się zaktualizować ilość produktu.";
                return RedirectToAction("Products");
            }
            TempData["message"] = "Przy przetwarzaniu danych wystąpił błąd.";
            return View(model);
        }

        public ActionResult CancelOrder(int id)
        {
            using (var context = new EfDbContext())
            {
                var order = context.Orders.Find(id);
                var user = UserManager.Users.Where(o => o.Id == order.UserId).FirstOrDefault();
                var canceledOrdersLimit = context.Settings.FirstOrDefault().QuantityOfCanceledOrdersLimit;
                var orderPositions = context.OrderPositions.Where(o => o.OrderId == id).ToList();
                foreach (var orderPosition in orderPositions)
                {
                    var product = context.Products.Where(o => o.Name == orderPosition.ProductName).FirstOrDefault();
                    product.Quantity += orderPosition.Amount;
                }
                user.UserData.QuantityOfCanceledOrders++;
                if(user.UserData.QuantityOfCanceledOrders >= canceledOrdersLimit)
                {
                    user.UserData.IsOnBlackList = true;
                }
                UserManager.Update(user);
                order.OrderStatus = OrderStatus.Anulowano;
                context.SaveChanges();
            }
            TempData["message"] = "Udało się anulować zamówienie";
            return RedirectToAction("Orders");
        }

        public ActionResult OrderDetails(int id)
        {
            OrderDetailsViewModel order = new OrderDetailsViewModel();
            using (var context = new EfDbContext())
            {
                order.Order = context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
                order.OrderPositions = context.OrderPositions.Where(o => o.OrderId == order.Order.OrderId).ToList();
            }

            return View(order);
        }

        public ActionResult Settings()
        {
            SettingsViewModel viewModel;
            if (db.Settings.FirstOrDefault() == null)
                viewModel = new SettingsViewModel();
            else
            {
                viewModel = new SettingsViewModel();
                viewModel.SettingsId = db.Settings.FirstOrDefault().Id;
                viewModel.QuantityOfProductsLimit = db.Settings.FirstOrDefault().QuantityOfProductsLimit;
                viewModel.QuantityOfCanceledOrdersLimit = db.Settings.FirstOrDefault().QuantityOfCanceledOrdersLimit;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Settings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EfDbContext())
                {
                    if (model.SettingsId == 0)
                    {
                        Settings newSettings = new Settings();
                        newSettings.QuantityOfProductsLimit = model.QuantityOfProductsLimit;
                        newSettings.QuantityOfCanceledOrdersLimit = model.QuantityOfCanceledOrdersLimit;
                        context.Entry(newSettings).State = EntityState.Added;
                        context.SaveChanges();
                        TempData["message"] = "Udało się zapisać zmiany.";
                        return RedirectToAction("Settings");
                    }
                    else
                    {
                        var settings = context.Settings.Find(1);
                        if (model.QuantityOfProductsLimit != null)
                        {
                            settings.QuantityOfProductsLimit = model.QuantityOfProductsLimit;
                            var users = UserManager.Users.ToList();
                            settings.QuantityOfCanceledOrdersLimit = model.QuantityOfCanceledOrdersLimit;
                            if (model.QuantityOfCanceledOrdersLimit == 0)
                            {
                                foreach (var user in users)
                                {
                                    if (user.UserData.IsOnBlackList)
                                    {
                                        user.UserData.IsOnBlackList = false;
                                        UserManager.Update(user);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var user in users)
                                {
                                    if (user.UserData.QuantityOfCanceledOrders < model.QuantityOfCanceledOrdersLimit)
                                    {
                                        if (user.UserData.IsOnBlackList)
                                        {
                                            user.UserData.IsOnBlackList = false;
                                            UserManager.Update(user);
                                        }
                                    }
                                    else
                                    {
                                        if (!user.UserData.IsOnBlackList)
                                        {
                                            user.UserData.IsOnBlackList = true;
                                            UserManager.Update(user);
                                        }
                                    }
                                }
                            }
                        }
                        var result = context.SaveChanges();
                        TempData["message"] = "Udało się zapisać zmiany.";
                        return RedirectToAction("Settings");
                    }
                }
            }
            TempData["message"] = "Nie udało się zapisać zmian.";
            return View(model);
        }

        public ActionResult BlackList()
        {
            var users = UserManager.Users.Where(o => o.UserData.IsOnBlackList).ToList();
            return View(users);
        }

        public ActionResult AddUserToBlackList(string id = null)
        {
            if(id == null)
            {
                var users = UserManager.Users.Where(o => o.UserData.IsOnBlackList == false).ToList();
                return View(users);
            }
            var user = UserManager.FindById(id);
            user.UserData.IsOnBlackList = true;
            UserManager.Update(user);
            TempData["message"] = "Udało się dodać użytkownika do czarnej listy";
            return RedirectToAction("BlackList");
        }

        public ActionResult DeleteUserFromBlackList(string id)
        {
            var user = UserManager.FindById(id);
            user.UserData.IsOnBlackList = false;
            var result = UserManager.Update(user);
            TempData["message"] = "Usunięto użytkownika z czarnej listy";
            return RedirectToAction("BlackList");
        }

        public ActionResult LockAccount(string id)
        {
            var user = UserManager.FindById(id);
            user.IsLocked = true;
            UserManager.Update(user);
            TempData["message"] = "Udało się dezaktywować konto użytkownika.";
            return RedirectToAction("Users");
        }

        public ActionResult UnlockAccount(string id)
        {
            var user = UserManager.FindById(id);
            user.IsLocked = false;
            UserManager.Update(user);
            TempData["message"] = "Udało się aktywować konto uzytkownika.";
            return RedirectToAction("Users");
        }
        #endregion
    }
}