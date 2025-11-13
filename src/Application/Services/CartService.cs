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
        private readonly IRepositoryBase<Product> _productRepositoryBase;

        public CartService(IRepositoryBase<Product> productRepositoryBase, ICartRepository cartRepository, IProductRepository productRepository, IItemCartRepository itemCartRepository, IRepositoryBase<User> userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _itemCartRepository = itemCartRepository;
            _userRepository = userRepository;
            _productRepositoryBase = productRepositoryBase;
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
            var productoReal = await _productRepositoryBase.GetByIdAsync(CreateItemCartDTO.ProductId);

            if (productoReal == null || producto.Enable == false) throw new Exception("Producto no existente.");
            if (CreateItemCartDTO.Quantity <= 0) throw new Exception("La cantidad a añadir debe ser mayor que cero.");


            var totalQuantity = itemExistente != null
                ? itemExistente.Quantity + CreateItemCartDTO.Quantity
                : CreateItemCartDTO.Quantity;


            if (productoReal.Stock < CreateItemCartDTO.Quantity)
                throw new Exception($"Stock insuficiente para añadir {CreateItemCartDTO.Quantity} unidades. Quedan {productoReal.Stock} disponibles.");

            ItemCart finalItem;

            if (itemExistente != null)
            {
                itemExistente.Quantity = totalQuantity; // Usar el valor calculado
                itemExistente.Subtotal = productoReal.Price * totalQuantity;
                await _itemCartRepository.UpdateAsync(itemExistente);
                finalItem = itemExistente;
            }
            else
            {
                var nuevoItem = new ItemCart
                {
                    CartId = CreateItemCartDTO.CartId,
                    ProductId = CreateItemCartDTO.ProductId,
                    Product = producto,
                    Quantity = CreateItemCartDTO.Quantity,
                    Subtotal = productoReal.Price * CreateItemCartDTO.Quantity
                };
                await _itemCartRepository.CreateAsync(nuevoItem);
                finalItem = nuevoItem;
            }

            productoReal.Stock -= CreateItemCartDTO.Quantity;
            await _productRepositoryBase.UpdateAsync(productoReal);

            var currentCartWithItems = await _cartRepository.GetByIdAsync(CreateItemCartDTO.CartId);
            await RecalculateCartTotal(currentCartWithItems);

            return ItemCartDTO.FromEntity(finalItem);
        }

        public async Task<CartDTO> DeleteItemCart(int cartId, int productId)
        {
            var item = await _itemCartRepository.GetByIdAsync(cartId, productId);
            var cart = await _cartRepository.GetByIdAsync(cartId);

            if (item == null)
                throw new NotFoundException("El ítem no se encontró en el carrito.");

            var productReal = await _productRepositoryBase.GetByIdAsync(productId);

            if (productReal != null)
            {
                productReal.Stock += item.Quantity;
                await _productRepositoryBase.UpdateAsync(productReal);
            }

            await _itemCartRepository.DeleteAsync(item);

            var updatedCart = await _cartRepository.GetByIdAsync(cartId);

            await RecalculateCartTotal(updatedCart);

            return CartDTO.FromEntity(updatedCart);
        }

        public async Task<CartDTO> ModifyItemCart(int cartId, int productId, bool incremented)
        {
            var product = await _productRepository.GetByIdProductsWithCategory(productId);
            var productReal = await _productRepositoryBase.GetByIdAsync(product.Id);
            var item = await _itemCartRepository.GetByIdAsync(cartId, productId);
            var cart = await _cartRepository.GetByIdAsync(cartId);

            if (item == null) throw new NotFoundException("Item no encontrado en el carrito.");
            if (productReal == null) throw new NotFoundException($"Producto con ID {product.Id} no encontrado.");

            int change = incremented ? 1 : -1;

            //Incremento
            if (incremented)
            {
                if (productReal.Stock <= 0)
                    throw new Exception("Stock Insuficiente.");

                item.Quantity += change;
                productReal.Stock -= change;
            }
            //Decremento
            else
            {
                if (item.Quantity + change <= 0)
                { 
                    return await DeleteItemCart(cartId, productId);
                }

                item.Quantity += change;
                productReal.Stock -= change;
            }

           
            item.Subtotal = productReal.Price * item.Quantity;
            await _itemCartRepository.UpdateAsync(item);

            await _productRepositoryBase.UpdateAsync(productReal);

            var currentCartWithItems = await _cartRepository.GetByIdAsync(cartId);

            await RecalculateCartTotal(currentCartWithItems);

            return CartDTO.FromEntity(currentCartWithItems);
        }

        private async Task RecalculateCartTotal(Cart cart)
        {
            cart.TotalPrice = cart.Items.Sum(i => i.Subtotal);
            await _cartRepository.UpdateAsync(cart);
        }
    
    }
}
