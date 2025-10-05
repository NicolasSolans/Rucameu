using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) 
        {
            _context = context;
        }

        //GET
        public Task<Product> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //POST
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        //UPDATE
        public Task<Product> UpdateAsync(Product updateProduct)
        {
            throw new NotImplementedException();
        }

        //DELETE
        public Task<Product> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
