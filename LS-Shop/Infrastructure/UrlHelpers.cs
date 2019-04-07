using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.Infrastructure
{
    public static class UrlHelpers
    {
        public static string IconsOfCategoriesPath(this UrlHelper helper, string nameOfIcon)
        {
            var iconsOfCategoriesFolder = AppConfig.IconsOfCategoriesFolder;
            var path = Path.Combine(iconsOfCategoriesFolder, nameOfIcon);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }

        public static string ImagesOfProductsPath(this UrlHelper helper, string nameOfImage)
        {
            var imagesOfProductsFolder = AppConfig.ImagesOfProductsFolder;
            var path = Path.Combine(imagesOfProductsFolder, nameOfImage);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }
    }
}