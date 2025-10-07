using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public virtual async Task<List<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int idEntity)
        {
            var findId = await _context.Set<T>().FindAsync(idEntity);
            return findId;
        }

        //public virtual async Task<List<T>> GetByNameAsync(string nameEntity)
        //{
        //    var product = await _context.Set<T>().FindAsync
        //}

        //POST
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        //UPDATE
        public virtual async Task UpdateAsync(T updateEntity)
        {
            _context.Set<T>().Update(updateEntity);
            await _context.SaveChangesAsync();
            

        }

        //DELETE
        public virtual async Task DisableAsync(T disableEntity)
        {
            _context.Set<T>().Update(disableEntity);
            await _context.SaveChangesAsync();
        }
    }
}
