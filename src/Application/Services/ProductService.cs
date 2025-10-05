using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
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


        public Task<List<ProductDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetById(int id)
        {
            throw new NotImplementedException();
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

        public Task<Product> Update(Product updateProduct)
        {
            throw new NotImplementedException();
        }
        public Task Delete(Product deleteProduct)
        {
            throw new NotImplementedException();
        }
    }
}
