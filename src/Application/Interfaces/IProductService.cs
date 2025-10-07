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
        Task<List<ProductDTO>> GetAllEnable();
        Task<ProductDTO> GetById(int id);
        Task<List<ProductDTO>> GetByName(string name);
        Task<List<ProductDTO>> GetByNameEnable(string name);

        //POST
        Task<ProductDTO> Create(CreateProductDTO newProduct);

        //UPDATE
        Task<ProductDTO> Update(CreateProductDTO updateProduct, int id);

        //DELETE
        Task<string> Disable(int id);
    }
}
