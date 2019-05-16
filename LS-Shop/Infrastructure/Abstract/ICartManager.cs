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
        List<PositionCart> GetCart();
        void AddToCart(int productId);
        int DeleteFromCart(int productId);
        int OneDeleteFromCart(int productId);
        decimal GetCartValue();
        int GetCartQuantity();
        Order CreateOrder(Order newOrder, string userEmail);
        void ClearCart();
    }
}
