using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserDeleted(User user, int adminId)
        {
            var admin = await _context.Users.OfType<Admin>()
                .FirstOrDefaultAsync(a => a.Id == adminId);
            if (admin == null || user == null) 
            {
                throw new NotImplementedException();
            }
            admin.UsersDeleted.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
