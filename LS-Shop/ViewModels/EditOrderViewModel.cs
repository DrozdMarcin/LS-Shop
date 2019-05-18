using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class EditOrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderPosition> OrderPosition { get; set; }

    }
}