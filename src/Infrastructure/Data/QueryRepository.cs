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
    public class QueryRepository : RepositoryBase<Query>, IQueryRepository
    {
        private readonly ApplicationDbContext _context;
        public QueryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Query>> GetAllAsync() //override me permite usar "GetAllAsync" con una implementación diferente a la de RepositoryBase
        {
            return await _context.Queries.Include(q => q.Cart).ToListAsync();
        }
    }
}
