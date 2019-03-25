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
    internal class ProductsInitializer : MigrateDatabaseToLatestVersion<ProductsContext, Configuration>
    {     

        public static void SeedProductsData(ProductsContext context)
        {
            var categories = new List<Category>
            {
                new Category()
                {
                    Name = "Słodycze",
                    Description = "Najwyższej jakości słodycze tylko z naturalnych składników",
                    NameOfImage = "slodyczne.png"
                },
                new Category()
                {
                    Name = "Białko", Description = "Naturalne białko różnego pochodzenia",
                    NameOfImage = "bialko.png"
                },
                new Category()
                {
                    Name = "Oleje", Description = "Największy wybór olei w Polsce",
                    NameOfImage = "oleje.png"
                },
                new Category()
                {
                     Name = "Yerba mate", Description = "Certyfikowana yerba mate w różnych wariantach",
                    NameOfImage = "yerba.png"
                },

                new Category()
                {
                    Name = "Owoce", Description = "Owoce prosto z ogródka",
                    NameOfImage = "owoce.png"
                }

            };

            categories.ForEach(c => context.Categories.AddOrUpdate(h=>h.Name,c));

            var products = new List<Product>
            {
                new Product()
                {
                    Name = "Yerba Kraus Organica",
                    Description = "Koszerna yerba niepalona z Brazylii",
                    Hidden = false,
                    DateOfAddition = DateTime.UtcNow,
                    CategoryId = 4,    
                    //Category = categories.FindLast(c=>c.Name.Equals("Yerba mate"))
                },

                new Product()
                {
                    Name = "Yerba Taragui",
                    Description = "Yerba klasyczna niepalona o ciekawym smaku",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Yerba mate"),  
                    CategoryId = 4,
                },

                new Product()
                {
                Name = "Oliwa z oliwek",
                Description = "Oliwa z oliwek pierwszego tłoczenia najwyższej jakości",
                Hidden = false,
                DateOfAddition = DateTime.Now,
                //Category = categories.Find(c => c.Name == "Oleje"),   
                    CategoryId = 3,
                },

                new Product()
                {
                    Name = "Białko WPC 99",
                    Description = "Bardzo skondensowane białko serwatkowe, aż 99%!",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Białko"),
                    CategoryId = 2,

                    Bestseller = true
                },

                new Product()
                {
                    Name = "Baton owsiany",
                    Description = "Baton z płatkami owsianymi, posypany suszonymi owocami oblanymi gorzką czekoladą",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },
                new Product()
                {
                    Name = "Cukierek",
                    Description = "cukierek",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "Żelki",
                    Description = "żelki",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "czekolada",
                    Description = "gorzka czekolada BIO",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "oscypki",
                    Description = "pyszne oscypki",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "wafle",
                    Description = "wafle",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "lizak",
                    Description = "lizak",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                //new Product()
                //{
                //    Name = "mordoklejka",
                //    Description = "mordoklejka",
                //    Hidden = false,
                //    DateOfAddition = DateTime.Now,

                //},

                new Product()
                {
                    Name = "landrynki",
                    Description = "landrynki",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    //Category = categories.Find(c => c.Name == "Słodycze"), 
                    CategoryId = 1
                },

                new Product()
                {
                    Name = "ananas",
                    Description = "ananasik",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 5,

                    Bestseller = true
                    
                },

                new Product()
                {
                    Name = "banan",
                    Description = "bananek",
                    Hidden = false,
                    DateOfAddition = DateTime.Now,
                    CategoryId = 5,

                },
            };

            products.ForEach(p => context.Products.AddOrUpdate(h => h.Name, p));

            context.SaveChanges();
        }
    }
}