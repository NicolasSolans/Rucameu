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
        //Task<List<CartDTO>> GetAll();
        Task<CartDTO> GetByUserId(int id);
        //Task<CartDTO> Update(UpdateCartDTO updateCart);
        Task<CartDTO> Delete(int id);
        Task<CartDTO> Create(CreateCartDTO newCart);

        //Operaciones específicas de ItemCart
        //Task<CartDTO> DeleteItemCart (int cartId, int itemId);
        //Task<CartDTO> AddItemCart(int cartId, ItemCartDTO newItemDTO);
        //Task<CartDTO> UpdateItemCart(int cartId, UpdateItemCartDTO UpdatedItemCartDTO);
        Task AddItemCart(CreateItemCartDTO CreateItemCartDTO);
    }
}
