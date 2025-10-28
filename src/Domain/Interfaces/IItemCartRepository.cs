using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemCartRepository : IRepositoryBase<ItemCart>
    {
        Task<ItemCart> GetByIdAsync(int cartId, int productId);
    }
}
