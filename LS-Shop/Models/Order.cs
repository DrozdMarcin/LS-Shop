using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wprowadź ulicę")]
        [StringLength(100)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime DateOfAddition { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal OrderValue { get; set; }

        public List<OrderPosition> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Completed
    }
   
}