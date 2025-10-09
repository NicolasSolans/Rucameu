using Domain.Interfaces;
using Infrastructure.Data;
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
        public virtual async Task<T> CreateAsync(T newT)
        {
            await _context.Set<T>().AddAsync(newT);
            await _context.SaveChangesAsync();
            return newT;
        }

        public virtual async Task DeleteAsync(T Ttoremove)
        {
            await _context.Set<T>().ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
           return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T UpdatedT)
        {
           _context.Set<T>().Update(UpdatedT);
            await _context.SaveChangesAsync();
        }
    }
}
