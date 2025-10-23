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
    public class CartService : ICartService
    {
        private readonly ICartRepocitory _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRepositoryBase<ItemCart> _itemCartRepositoryBase;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IRepositoryBase<ItemCart> itemCartRepository )
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _itemCartRepositoryBase = itemCartRepository;
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
                return await _itemCartRepositoryBase.UpdateAsync(itemExistente);
            }
        
                var nuevoItem = new ItemCart();
                nuevoItem.CartId = CreateItemCartDTO.CartId;
                nuevoItem.ProductId = CreateItemCartDTO.ProductId;
                nuevoItem.Product = producto;
                nuevoItem.Quantity = CreateItemCartDTO.Quantity;
                nuevoItem.Subtotal = producto.Price * CreateItemCartDTO.Quantity;
                await _itemCartRepositoryBase.CreateAsync(nuevoItem);
                return ItemCartDTO.FromEntity(nuevoItem);
            
        }
    }
}
