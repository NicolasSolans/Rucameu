using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        //GET
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetAllAsync();

        //POST
        Task<T> CreateAsync(T entity);

        //UPDATE
        Task<Product> UpdateAsync(Product updateProduct);

        //DELETE
        Task<Product> DeleteAsync(int id);
    }
}
