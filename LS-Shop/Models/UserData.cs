using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LS_Shop.Models
{
    public class UserData
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public int QuantityOfCanceledOrders { get; set; } = 0;
        public bool IsOnBlackList { get; set; } = false;
    }
}