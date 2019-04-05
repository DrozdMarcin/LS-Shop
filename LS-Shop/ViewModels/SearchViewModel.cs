using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LS_Shop.Models;

namespace LS_Shop.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}