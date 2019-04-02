using LS_Shop.Data_Access_Layer;
using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LS_Shop.Infrastructure
{
    public class CartMenager
    {
        private EfDbContext db;
        private ISessionManager session;
        public CartMenager(ISessionManager session, EfDbContext db)
        {
            this.session = session;
            this.db = db;
        }

        //Metody do obsługi koszyka

        //pobieranie zawartości koszyka
        public List<PositionCart> TakeCart()
        {
            List<PositionCart> cart;

            if (session.Get<List<PositionCart>>(Consts.CartSessionKey) == null)  //gdy pusto
            {
                cart = new List<PositionCart>();
            }
            else //gdy będą jakieś elementy pobieramy wszsytkie pozycje koszyka z sesji
            {
                cart = session.Get<List<PositionCart>>(Consts.CartSessionKey) as List<PositionCart>;
            }

            return cart;
        }

        //dodawanie do koszyka
        public void AddToCart(int productID)
        {
            var cart = TakeCart();
            var possitionCart = cart.Find(k => k.Product.ProductId == productID);

            if (possitionCart != null)
            {
                possitionCart.Value++;
            }
            else
            {
                var productTBAdded = db.Products.Where(k => k.ProductId == productID).SingleOrDefault();

                //sprawdzamy czy pobrało produkt
                if (productTBAdded != null)
                {
                    //pobrano
                    var newPossitionCart = new PositionCart()
                    {

                        Product = productTBAdded,
                        Amount = 1,
                        Value = productTBAdded.Price
                    };

                    cart.Add(newPossitionCart);
                } 
            } 
              //uaktualnienie sesji
              session.Set(Consts.CartSessionKey, cart);
        }

        //usuwanie z koszyka
        public int DeleteFromCart(int productID)
        {
            var cart = TakeCart();
            var positionCart = cart.Find(k => k.Product.ProductId == productID);

            if(positionCart != null)
            {
                if(positionCart.Amount > 1)
                {
                    positionCart.Amount--;
                    return positionCart.Amount;
                }
                else
                {
                    cart.Remove(positionCart);
                }
            }
            return 0;
        }

        //wartość koszyka
        public decimal TakeValueCart()
        {
            var cart = TakeCart();
            return cart.Sum(k => (k.Amount * k.Product.Price));
        }

        //zwracanie ilości pozycji w koszyku
        public int TakeValuePossitionCart()
        {
            var cart = TakeCart();
            var amount = cart.Sum(k => k.Amount);
            return amount;
        }


        //Metoda tworzenia nowe zamowienie
       public Order CreateOrder(Order newOrder, string userEmail)
        {
            var cart = TakeCart();
            newOrder.DateOfAddition = DateTime.Now;
            newOrder.Email = userEmail;
            
            //dodanie zamówienia
            db.Orders.Add(newOrder);

            //dodanie pozycji zamowienia
            if (newOrder.OrderPosition == null)
                newOrder.OrderPosition = new List<OrderPosition>();

            decimal cartValue = 0;

            foreach( var cartElement in cart)
            {
                //tworzenie nowej pozycji zamowienia
                var newPositionOrder = new OrderPosition()
                {
                    ProductId = cartElement.Product.ProductId,
                    Amount = cartElement.Amount,
                    PurchasePrice = cartElement.Product.Price
                };

                cartValue += (cartElement.Amount * cartElement.Product.Price);
                newOrder.OrderPosition.Add(newPositionOrder);
           }
            newOrder.OrderValue = cartValue;
            db.SaveChanges();
            return newOrder;
    }
    }
}
