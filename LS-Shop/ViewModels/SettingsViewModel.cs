using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class SettingsViewModel
    {
        public int SettingsId { get; set; }
        [Range(0,int.MaxValue, ErrorMessage = "Limit nie może być mniejszy od 0.")]
        public int? QuantityOfProductsLimit { get; set; }
    }
}