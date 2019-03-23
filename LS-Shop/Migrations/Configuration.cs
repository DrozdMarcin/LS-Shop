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
        }

        protected override void Seed(LS_Shop.Data_Access_Layer.ProductsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var categories = new List<Category>
            {
                new Category()
                {
                    CategoryId = 1, Name = "S³odycze",
                    Description = "Najwy¿szej jakoœci s³odycze tylko z naturalnych sk³adników",
                    NameOfImage = "slodyczne.png"
                },
                new Category()
                {
                    CategoryId = 2, Name = "Bia³ko", Description = "Naturalne bia³ko ró¿nego pochodzenia",
                    NameOfImage = "bialko.png"
                },
                new Category()
                {
                    CategoryId = 3, Name = "Oleje", Description = "Najwiêkszy wybór olei w Polsce",
                    NameOfImage = "oleje.png"
                },
                new Category()
                {
                    CategoryId = 4, Name = "Yerba mate", Description = "Certyfikowana yerba mate w ró¿nych wariantach",
                    NameOfImage = "yerba.png"
                }
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(c));
            context.SaveChanges();
        }
    }
}
