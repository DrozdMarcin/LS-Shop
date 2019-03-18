using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }  //foreign key
        public string Name { get; set; }
        public DateTime DateOfAddition { get; set; }
        public string NameOfImage { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Bestseller { get; set; }
        public bool Hidden { get; set; }

        public virtual Category Category { get; set; }
    }
}