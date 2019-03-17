using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
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