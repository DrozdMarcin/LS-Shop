using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class NewProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}