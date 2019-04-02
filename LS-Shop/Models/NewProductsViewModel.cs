using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class NewProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}