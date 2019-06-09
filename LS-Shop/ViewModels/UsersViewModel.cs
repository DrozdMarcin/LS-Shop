using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class UsersViewModel
    {
        public ApplicationUser User { get; set; }
        public bool IsAdministrator { get; set; }
    }
}