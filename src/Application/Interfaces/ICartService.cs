using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface ICartService
    {  
        //CRUD básico
        Task<CartDTO> GetByUserId(int id);
        Task<CartDTO> GetById(int id);
        Task<CartDTO> Delete(int id);
        Task<CartDTO> Create(CreateCartDTO newCart);

        //Operaciones específicas de ItemCart
        Task<ItemCartDTO> AddItemCart(CreateItemCartDTO CreateItemCartDTO);
        Task<CartDTO> DeleteItemCart (int cartId, int itemId);
        //Task<CartDTO> UpdateItemCart(int cartId, UpdateItemCartDTO UpdatedItemCartDTO);
    }
}
