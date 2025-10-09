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
    public class ProductService : IProductService
    {
        private readonly IRepositoryBase<Product> _repositoryBase;

        public ProductService(IRepositoryBase<Product> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }


        public async Task<List<ProductDTO>> GetAll()
        {
            var productList = await _repositoryBase.GetAllAsync();
            return ProductDTO.CreateListDTO(productList);
        }

        public async Task<List<ProductDTO>> GetAllEnable()
        {
            var productList = await _repositoryBase.GetAllAsync();
            var EnableProducts = productList.Where(p => p.Enable == true).ToList();

            return ProductDTO.CreateListDTO(EnableProducts);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var findProduct = await _repositoryBase.GetByIdAsync(id);

            if (findProduct == null)
            {
                throw new NotFoundException($"No se encontro el poducto con id {id}");
            }

            return ProductDTO.CreateDTO(findProduct);
        }

        public async Task<List<ProductDTO>> GetByName(string name)
        {
            var allProducts = await _repositoryBase.GetAllAsync();
            var productName = allProducts.Where(p => p.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();

            return ProductDTO.CreateListDTO(productName);
        }

        public async Task<List<ProductDTO>> GetByNameEnable(string name)
        {
            var allProducts = await _repositoryBase.GetAllAsync();
            var productName = allProducts.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && p.Enable == true).ToList();

            return ProductDTO.CreateListDTO(productName);
        }

        public async Task<ProductDTO> Create(CreateProductDTO newProduct)
        {
            var product = new Product();

            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.Stock = newProduct.Stock;
            product.ImgUrl = newProduct.ImgUrl;

            var productAdd = await _repositoryBase.CreateAsync(product);
            return ProductDTO.CreateDTO(productAdd);
        }

        public async Task<ProductDTO> Update(CreateProductDTO updateProduct, int id)
        {
            var findProduct = await _repositoryBase.GetByIdAsync(id);

            if (findProduct == null)
            {
                throw new NotFoundException($"No se encontro el producto con id {id}");
            }

            findProduct.Name = updateProduct.Name;
            findProduct.Description = updateProduct.Description;
            findProduct.Price = updateProduct.Price;
            findProduct.Stock = updateProduct.Stock;
            findProduct.ImgUrl = updateProduct.ImgUrl;

            await _repositoryBase.UpdateAsync(findProduct);
            return ProductDTO.CreateDTO(findProduct);
        }
        public async Task<string> Disable(int id)
        {
            var findProduct = await _repositoryBase.GetByIdAsync(id);

            if (findProduct == null)
            {
                throw new NotFoundException($"No se encontro el producto con id {id}");
            }

            findProduct.Enable = false;
            await _repositoryBase.DisableAsync(findProduct);
            return "Producto borrado correctamente";

        }
    }
}
