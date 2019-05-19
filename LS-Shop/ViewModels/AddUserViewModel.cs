using LS_Shop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LS_Shop.ViewModels
{
    public class AddUserViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public string UserRole { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        public string Password { get; set; }
    }
}