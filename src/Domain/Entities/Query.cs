using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Query
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateConsult { get; set; } = DateTime.Now;
        public int CartId { get; set; } // FK
        public Cart Cart { get; set; } // Navigation property
        public bool Status { get; set; } = false;
    }
}
