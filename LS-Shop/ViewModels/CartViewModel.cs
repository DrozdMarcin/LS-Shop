using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class CartViewModel
    {
        public List<PositionCart> PositionCart { get; set; }
        public decimal TotalValue { get; set; }
    }
}