using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
        public decimal TotalPrice 
        {
            get
            {
                return Items.Sum(item => item.Subtotal);
            }
            set { }
        }

        public static CartDTO FromEntity(Cart cart)
        {
            var dto = new CartDTO();
            dto.Id = cart.Id;
            dto.UserId = cart.UserId;
            dto.User = UserDTO.FromEntity(cart.User);
            dto.Items = ItemCartDTO.CreateListDTO(cart.Items);
            dto.TotalPrice = cart.TotalPrice;
            return dto;
        }

        //devuelve todos los carritos en una lista
        //public static List<CartDTO> CreateListDTO(List<Cart> carts)
        //{
        //    var dtoList = new List<CartDTO>();
        //    foreach (var c in carts)
        //    {
        //        dtoList.Add(FromEntity(c));
        //    }
        //    return dtoList;
        //}
    }
}
