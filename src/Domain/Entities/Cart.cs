using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<ItemCart> Items { get; set; } = new List<ItemCart>();
        public Query? Query { get; set; } // Relación 0:1
        public decimal TotalPrice 
        {
            get
            {
                return Items.Sum(item => item.Subtotal);
            }
            set { }
        }
    }
}
