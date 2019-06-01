using LS_Shop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LS_Shop.Data_Access_Layer
{
    public class EfDbContext : IdentityDbContext<ApplicationUser>
    {
        public EfDbContext() : base("ProductsContext")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPosition> OrderPositions { get; set; }
        public DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static EfDbContext Create()
        {
            return new EfDbContext();
        }
    }
}