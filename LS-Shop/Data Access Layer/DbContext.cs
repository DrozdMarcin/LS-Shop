using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using LS_Shop.Migrations;
using LS_Shop.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LS_Shop.Data_Access_Layer
{
    public class DbContext : IDbContext
    {
        #region private members
        private EfDbContext context = new EfDbContext();
        #endregion

        #region properties
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return context.Categories;
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders;
            }
        }

        public IEnumerable<OrderPosition> OrderPositions
        {
            get
            {
                return context.OrderPositions;
            }
        }

        public IEnumerable<Settings> Settings
        {
            get
            {
                return context.Settings;
            }
        }
        #endregion

        #region public methods
        public void Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
        #endregion
    }
}