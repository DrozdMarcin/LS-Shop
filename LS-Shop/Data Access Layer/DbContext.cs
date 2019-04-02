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
        private EfDbContext context = new EfDbContext();

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
    }
}