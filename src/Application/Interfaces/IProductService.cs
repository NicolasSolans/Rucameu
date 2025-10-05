using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        //GET
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
        
        //POST
        Task<ProductDTO> Create(CreateProductDTO newProduct);

        //UPDATE
        Task<Product> Update(Product updateProduct);

        //DELETE
        Task Delete(Product deleteProduct);
    }
}
