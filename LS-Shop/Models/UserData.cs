using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class UserData
    {
        [Required(ErrorMessage = "Podaj imie")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Podaj adres")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Podaj miasto")]
        public string City { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Zły format numeru")]
        public string Phone { get; set; }
    }
}