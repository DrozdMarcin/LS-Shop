//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LS_Shop.Data_Access_Layer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderPosition = new HashSet<OrderPosition>();
        }
    
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfAddition { get; set; }
        public string NameOfImage { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Bestseller { get; set; }
        public bool Hidden { get; set; }
        public string ApplicationUser_Id { get; set; }
        public int Quantity { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPosition> OrderPosition { get; set; }
    }
}
