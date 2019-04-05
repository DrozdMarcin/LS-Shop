using LS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Shop.Data_Access_Layer
{
    public interface IDbContext
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<Order> Orders { get; }
        IEnumerable<OrderPosition> OrderPositions { get; }
        void Add(Order order);
    }
}
