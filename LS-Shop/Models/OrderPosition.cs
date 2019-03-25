using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LS_Shop.Models
{
    public class OrderPosition
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //attributes not to assign Ids by the server
        public int OrderPositionId { get; set; }
        public int OrderId { get; set; }  // foreign key
        public int ProductId { get; set; }  // foreign key
        public int Amount { get; set; }  
        public decimal PurchasePrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}