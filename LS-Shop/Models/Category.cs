using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfImage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}