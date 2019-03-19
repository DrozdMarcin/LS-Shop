using System.ComponentModel.DataAnnotations;

namespace LS_Shop.Models
{
    public class OrderPosition
    {
        [Key]
        public int OrderPositionId { get; set; }
        public int OrderId { get; set; }  // foreign key
        public int ProductId { get; set; }  // foreign key
        public int Amount { get; set; }  
        public decimal PurchasePrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}