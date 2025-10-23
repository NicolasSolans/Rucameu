using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRepositoryBase<ItemCart> _itemCartRepository;
        public CartService(ICartRepository cartRepository, IRepositoryBase<ItemCart> itemCartRepository)
        { 
            _cartRepository = cartRepository;
            _itemCartRepository = itemCartRepository;
        }

        public async Task<Cart> GetByUserId(int UserId)
        {
            return await _cartRepository.GetByUserIdAsync(UserId);
        }

        public async Task<Cart> GetById(int id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task<Cart> Create(Cart newCart)
        {
            return await _cartRepository.CreateAsync(newCart);
        }

        public async Task<Cart> Delete(int id)
        {
            var cartToDelete = await _cartRepository.GetByIdAsync(id);
            if (cartToDelete == null)
            {
                throw new NotFoundException($"Carrito con {id} no encontrado.");
            }
            await _cartRepository.DeleteAsync(cartToDelete);
            return cartToDelete;
        }

        public async Task<Cart> Update(Cart updatedCart)
        {
            var cartToUpdate = await _cartRepository.GetByIdAsync(updatedCart.Id);
            if (cartToUpdate == null)
            {
                throw new NotFoundException($"Carrito con id {updatedCart.Id} no encontrado.");
            }
            await _cartRepository.UpdateAsync(updatedCart);
            return updatedCart;
        }
    }
