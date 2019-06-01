using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }

        public int? QuantityOfProductsLimit { get; set; }
    }
}