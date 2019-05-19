using LS_Shop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS_Shop.ViewModels
{
    public class UsersInRoleViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public IdentityRole Role { get; set; }
    }
}