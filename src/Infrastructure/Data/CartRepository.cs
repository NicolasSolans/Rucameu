using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Cart> GetByUserIdAsync(int userId)
        {
            return await _context.Carts
                                 .Include(c => c.Items)
                                 .OrderByDescending(c => c.Id)
                                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public override async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts
                                 .Include(c => c.Items)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
