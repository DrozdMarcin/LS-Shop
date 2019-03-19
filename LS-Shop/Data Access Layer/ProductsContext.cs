using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using LS_Shop.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LS_Shop.Data_Access_Layer
{
    public class ProductsContext : IdentityDbContext<ApplicationUser>
    {
        public ProductsContext() : base("ProductsContext")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPosition> OrderPositions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static ProductsContext Create()
        {
            return new ProductsContext();
        }
    }
}