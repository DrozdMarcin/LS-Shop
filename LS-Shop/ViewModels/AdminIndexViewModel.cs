using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<Product> ProductsWithLowQuantity { get; set; }
        public List<Order> LatestOrders { get; set; }
    }
}