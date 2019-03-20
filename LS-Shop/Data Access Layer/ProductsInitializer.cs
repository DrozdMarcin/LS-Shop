using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using LS_Shop.Migrations;
using LS_Shop.Models;

namespace LS_Shop.Data_Access_Layer
{
    public class ProductsInitializer : MigrateDatabaseToLatestVersion<ProductsContext,Configuration>
    {
        public static void SeedProductsData(ProductsContext context)
        {
            var categories = new List<Category>
            {
                new Category()
                {
                    CategoryId = 1, Name = "Słodycze",
                    Description = "Najwyższej jakości słodycze tylko z naturalnych składników",
                    NameOfImage = "slodyczne.png"
                },
                new Category()
                {
                    CategoryId = 2, Name = "Białko", Description = "Naturalne białko różnego pochodzenia",
                    NameOfImage = "bialko.png"
                },
                new Category()
                {
                    CategoryId = 3, Name = "Oleje", Description = "Największy wybór olei w Polsce",
                    NameOfImage = "oleje.png"
                },
                new Category()
                {
                    CategoryId = 4, Name = "Yerba mate", Description = "Certyfikowana yerba mate w różnych wariantach",
                    NameOfImage = "yerba.png"
                }
            };

            categories.ForEach(c=>context.Categories.AddOrUpdate(c));
            context.SaveChanges();

        }
    }
}