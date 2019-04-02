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
            var postitionCart = cart.Find(k => k.Product.ProductId == productID);

            if(postitionCart != null)
            {
                if(postitionCart.Amount > 1)
                {
                    postitionCart.Amount--;
                    return postitionCart.Amount;
                }
                else
                {
                    cart.Remove(postitionCart);
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

       
    }
}
