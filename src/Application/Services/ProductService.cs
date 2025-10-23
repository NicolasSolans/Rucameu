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
        private readonly IRepositoryBase<Product> _repositoryBaseProduct;
        private readonly IRepositoryBase<Category> _repositoryBaseCategory;
        private readonly IProductRepository _productRepository;

        public ProductService(IRepositoryBase<Product> repositoryBaseProduct, IRepositoryBase<Category> repositoryBaseCategory, IProductRepository productRepository)
        {
            _repositoryBaseProduct = repositoryBaseProduct;
            _repositoryBaseCategory = repositoryBaseCategory;
            _productRepository = productRepository;
        }


        public async Task<List<ProductDTO>> GetAll()
        {
            var productList = await _productRepository.GetAllProductsWithCategory();
            return ProductDTO.CreateListDTO(productList);
        }

        public async Task<List<ProductDTO>> GetAllEnable()
        {
            var productList = await _productRepository.GetAllProductsWithCategory();
            var EnableProducts = productList.Where(p => p.Enable == true).ToList();

            return ProductDTO.CreateListDTO(EnableProducts);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var findProduct = await _productRepository.GetByIdProductsWithCategory(id);

            if (findProduct == null)
            {
                throw new NotFoundException($"No se encontro el poducto con id {id}");
            }

            return ProductDTO.FromEntity(findProduct);
        }

        public async Task<List<ProductDTO>> GetByName(string name)
        {
            var allProducts = await _productRepository.GetAllProductsWithCategory();
            var productName = allProducts.Where(p => p.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();

            return ProductDTO.CreateListDTO(productName);
        }

        public async Task<List<ProductDTO>> GetByNameEnable(string name)
        {
            var allProducts = await _productRepository.GetAllProductsWithCategory();
            var productName = allProducts.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && p.Enable == true).ToList();

            return ProductDTO.CreateListDTO(productName);
        }

        public async Task<ProductDTO> Create(CreateProductDTO newProduct)
        {
            var categoryId = await _repositoryBaseCategory.GetByIdAsync(newProduct.CategoryId);
            if (categoryId == null) throw new Exception("La categoria no existe");

            var product = new Product();

            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.Stock = newProduct.Stock;
            product.ImgUrl = newProduct.ImgUrl;
            product.Category = categoryId;
            

            var productAdd = await _repositoryBaseProduct.CreateAsync(product);
            return ProductDTO.FromEntity(productAdd);
        }

        public async Task<ProductDTO> Update(UpdateProductDTO updateProduct)
        {
            var findProduct = await _productRepository.GetByIdProductsWithCategory(updateProduct.Id);
            var findCategory = await _repositoryBaseCategory.GetByIdAsync(updateProduct.CategoryId);

            if (findProduct == null) throw new NotFoundException($"No se encontro el producto con id {updateProduct.Id}");
            if (findCategory == null) throw new NotFoundException("La categoria no existe");

            findProduct.Name = updateProduct.Name;
            findProduct.Description = updateProduct.Description;
            findProduct.Price = updateProduct.Price;
            findProduct.Stock = updateProduct.Stock;
            findProduct.ImgUrl = updateProduct.ImgUrl;
            findProduct.CategoryId = updateProduct.CategoryId;

            await _repositoryBaseProduct.UpdateAsync(findProduct);
            return ProductDTO.FromEntity(findProduct);
        }
        public async Task<string> Disable(int id)
        {
            var findProduct = await _repositoryBaseProduct.GetByIdAsync(id);

            if (findProduct == null)
            {
                throw new NotFoundException($"No se encontro el producto con id {id}");
            }

            findProduct.Enable = false;
            await _repositoryBaseProduct.DisableAsync(findProduct);
            return "Producto borrado correctamente";

        }
    }
}
