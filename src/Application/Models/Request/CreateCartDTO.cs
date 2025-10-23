using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateCartDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
        public decimal? TotalPrice
        {
            get
            {
                return Items.Sum(item => item.Subtotal);
            }
            set { }
        }

    }
}
