using LS_Shop.Models;

namespace LS_Shop.Models
{
    public class PositionCart
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public decimal Value { get; set; }
    }

}