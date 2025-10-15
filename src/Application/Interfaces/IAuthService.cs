using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<User?> ValidateCredentialsAsync(string email, string password);
        string GenerateJwtToken(User user);
    }
}
