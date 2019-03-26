using System.Data.Entity.Migrations.Model;
using LS_Shop.Data_Access_Layer;

namespace LS_Shop.Migrations
{
    using LS_Shop.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LS_Shop.Data_Access_Layer.ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LS-Shop.Data_Access_Layer.ProductsContext";
        }

        public Configuration(ProductsContext context) : base()
        {
            Seed(context);
        }

        protected override void Seed(LS_Shop.Data_Access_Layer.ProductsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            ProductsInitializer.SeedProductsData(context);
        }
    }
}
