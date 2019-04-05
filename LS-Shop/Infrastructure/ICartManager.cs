using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Shop.Infrastructure
{
    public interface ICartManager
    {
        List<PositionCart> TakeCart();
        void AddToCart(int productID);
        int DeleteFromCart(int productID);
        decimal TakeValueCart();
        int TakeValuePossitionCart();
        Order CreateOrder(Order newOrder, string userEmail);
        void ClearCart();
    }
}
