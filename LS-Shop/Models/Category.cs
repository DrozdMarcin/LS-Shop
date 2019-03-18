using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę kategorii")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadź opis kategorii")]
        public string Description { get; set; }
        public string NameOfImage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}