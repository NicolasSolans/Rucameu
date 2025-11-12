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
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IItemCartRepository _itemCartRepository;
        private readonly IRepositoryBase<User> _userRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IItemCartRepository itemCartRepository, IRepositoryBase<User> userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _itemCartRepository = itemCartRepository;
            _userRepository = userRepository;
        }
      
        public async Task<CartDTO> GetByUserId(int UserId)
        {

            var cart = await _cartRepository.GetByUserIdAsync(UserId);
            if (cart == null)
            {
                throw new NotFoundException($"Carrito para el usuario con id {UserId} no encontrado.");
            }
            return CartDTO.FromEntity(cart);
        }

        public async Task<CartDTO> GetById(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            return CartDTO.FromEntity(cart);

        }

        public async Task<CartDTO> Create(CreateCartDTO newCartDTO)
        {
            var user = await _userRepository.GetByIdAsync(newCartDTO.UserId);
            var cart = new Cart
            {
                UserId = newCartDTO.UserId,
                User = user,
                //Items = new List<ItemCart>(),
                TotalPrice = 0
            };
            await _cartRepository.CreateAsync(cart);
            return CartDTO.FromEntity(cart);

        }

        public async Task<CartDTO> Delete(int id)
        {
            var cartToDelete = await _cartRepository.GetByIdAsync(id);
            if (cartToDelete == null)
            {
                throw new NotFoundException($"Carrito con {id} no encontrado.");
            }
            await _cartRepository.DeleteAsync(cartToDelete);
            return CartDTO.FromEntity(cartToDelete);

        }

        public async Task<CartDTO> Update(Cart updatedCart)
        {
            var cartToUpdate = await _cartRepository.GetByIdAsync(updatedCart.Id);
            if (cartToUpdate == null)
            {
                throw new NotFoundException($"Carrito con id {updatedCart.Id} no encontrado.");
            }
            await _cartRepository.UpdateAsync(updatedCart);
            return CartDTO.FromEntity(updatedCart);

        }

        public async Task<ItemCartDTO> AddItemCart(CreateItemCartDTO CreateItemCartDTO)
        {
            var cart = await _cartRepository.GetByIdAsync(CreateItemCartDTO.CartId);

            if (cart == null) throw new NotFoundException("Carrito no encontrado");

            var itemExistente = cart.Items.FirstOrDefault(i => i.ProductId == CreateItemCartDTO.ProductId);
            var producto = await _productRepository.GetByIdProductsWithCategory(CreateItemCartDTO.ProductId);
            if (producto.Enable == false) throw new Exception("Producto no existente.");
            if (itemExistente != null)
            {

                itemExistente.Quantity += CreateItemCartDTO.Quantity;
                itemExistente.Subtotal = producto.Price * itemExistente.Quantity;
                await _itemCartRepository.UpdateAsync(itemExistente);
                return ItemCartDTO.FromEntity(itemExistente);
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
        public async Task<CartDTO> DeleteItemCart(int cartId, int productId)
        {
            var item = await _itemCartRepository.GetByIdAsync(cartId, productId);
            var cart = await _cartRepository.GetByIdAsync(cartId);

            await _itemCartRepository.DeleteAsync(item);

            cart.TotalPrice = cart.Items
                .Where(i => !(i.CartId == cartId && i.ProductId == productId))
                .Sum(i => i.Subtotal);

            await _cartRepository.UpdateAsync(cart);

            var updatedCart = await _cartRepository.GetByIdAsync(cartId);
            return CartDTO.FromEntity(updatedCart);
        }

        public async Task<CartDTO> ModifyItemCart(int cartId, int productId, bool incremented)
        {
            var item = await _itemCartRepository.GetByIdAsync(cartId, productId);
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if(item == null) throw new NotFoundException("Item no encontrado en el carrito.");

            if (incremented)
            {
                item.Quantity += 1; 
                
            }else
            {
                item.Quantity -= 1;
                if (item.Quantity <= 0)
                {
                    await DeleteItemCart(cartId, productId);
                }
                }

            item.Subtotal = item.Product.Price * item.Quantity;
            cart.TotalPrice = cart.Items
                .Where(i => !(i.CartId == cartId && i.ProductId == productId))
                .Sum(i => i.Subtotal);
            await _cartRepository.UpdateAsync(cart);
            var updatedCart = await _cartRepository.GetByIdAsync(cartId);
            return CartDTO.FromEntity(updatedCart);
        }
    }
}
