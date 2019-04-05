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
        public bool AnySearchingProductExists { get; set; } = false;
        public string SearchText { get; set; }
    }
}