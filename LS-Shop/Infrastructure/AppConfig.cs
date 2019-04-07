using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LS_Shop.Infrastructure
{
    public class AppConfig
    {
        private static string _imagesOfProductsFolder = ConfigurationManager.AppSettings["ImagesOfProducts"];

        public static string ImagesOfProductsFolder
        {
            get { return _imagesOfProductsFolder; }
        }

        private static string _iconsOfCategoriesFolder = ConfigurationManager.AppSettings["IconsOfCategories"];

        public static string IconsOfCategoriesFolder
        {
            get { return _iconsOfCategoriesFolder; }
        }
    }
}