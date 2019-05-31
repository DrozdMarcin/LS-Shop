using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class DeliveryViewModel
    {
        public int ProductId { get; set; }
        public int DeliveredQuantity { get; set; }
    }
}