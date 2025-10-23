using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService
    {
        private readonly ICartRepocitory _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRepositoryBase<ItemCart> _itemCartRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IRepositoryBase<ItemCart> itemCartRepository )
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
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
                throw new NotFoundException($"Cart with id {id} not found.");
            }
            await _cartRepository.DeleteAsync(cartToDelete);
            return cartToDelete;
        }

        public async Task<Cart> Update(Cart updatedCart)
        {
            var cartToUpdate = await _cartRepository.GetByIdAsync(updatedCart.Id);
            if (cartToUpdate == null)
            {
                throw new NotFoundException($"Cart with id {updatedCart.Id} not found.");
            }
            await _cartRepository.UpdateAsync(updatedCart);
            return updatedCart;
        }

        public async Task<ItemCartDTO> AddItemCart(CreateItemCartDTO CreateItemCartDTO)
        {
            var cart = await _cartRepository.GetByIdAsync(CreateItemCartDTO.CartId);

            if (cart == null) throw new NotFoundException("Carrito no encontrado");

            var itemExistente = cart.Items.FirstOrDefault(i => i.ProductoId == CreateItemCartDTO.ProductId);
            var producto = await _productRepository.GetByIdProductsWithCategory(CreateItemCartDTO.ProductId);
            if (itemExistente != null)
            {
                
                itemExistente.Quantity += CreateItemCartDTO.Quantity;
                return await _itemCartRepository.UpdateAsync(itemExistente);
            }
        
                var nuevoItem = new ItemCart();
                nuevoItem.CartId = CreateItemCartDTO.CartId;
                nuevoItem.ProductId = CreateItemCartDTO.ProductId;
                nuevoItem.Product = producto;
                nuevoItem.Quantity = CreateItemCartDTO.Quantity;
                nuevoItem.Subtotal = producto.Price * CreateItemCartDTO.Quantity;
                await _itemCartRepository.CreateAsync(nuevoItem);
                return ItemCartDTO.FromEntity(nuevoItem);
            
        }
    }
}
