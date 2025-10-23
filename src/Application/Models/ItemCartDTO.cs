using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class ItemCartDTO
    {
        public int CartId { get; set; }
        public ProductDTO ProductDTO { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public static ItemCartDTO FromEntity(ItemCart itemCart)
        {
            var item = new ItemCartDTO();
            item.CartId = itemCart.CartId;
            item.ProductDTO = ProductDTO.FromEntity(itemCart.Product);
            item.Quantity = itemCart.Quantity;
            item.Subtotal = itemCart.Subtotal;
            return item;
        }

        public static List<ItemCartDTO> CreateListDTO(List<ItemCart> itemCartList)
        {
            var dtoList = new List<ItemCartDTO>();

            foreach (var i in itemCartList)
            {
                dtoList.Add(FromEntity(i));
            }

            return dtoList;
        }
    }
}
