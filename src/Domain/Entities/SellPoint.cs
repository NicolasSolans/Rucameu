using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SellPoint
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Location_link { get; set; }
        public DateTime Date { get; set; }
        public string? Images { get; set; }
    }
}
