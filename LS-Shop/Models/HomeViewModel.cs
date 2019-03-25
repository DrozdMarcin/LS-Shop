using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Product> News { get; set; }
        public IEnumerable<Product> Bestsellers { get; set; }
     }
}