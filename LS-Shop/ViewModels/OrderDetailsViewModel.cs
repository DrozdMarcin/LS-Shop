using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class OrderDetailsViewModel
    {
        public List<OrderPosition> OrderPositions { get; set; }
        public Order Order { get; set; }
    }
}