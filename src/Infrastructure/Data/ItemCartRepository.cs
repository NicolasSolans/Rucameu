using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ItemCartRepository : RepositoryBase<ItemCart>, IItemCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ItemCart> GetByIdAsync(int cartId, int productId)
        {
            return await _context.ItemCarts
                .FirstOrDefaultAsync(ic => ic.CartId == cartId && ic.ProductId == productId);
        }
    }
}
