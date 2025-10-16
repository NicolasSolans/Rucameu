using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string> Authenticate(AuthenticationRequestDTO authenticationRequestDTO);
        Task<bool> ValidateIdUser(int userId, int Id);
    }
}
