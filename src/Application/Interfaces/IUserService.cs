using Application.Models.Request;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Register(CreateUserDTO createUserDTO);
        Task<AdminDTO> RegisterAdmin(CreateAdminDTO createAdminDTO);
        Task LogOut(int userId);
        Task<UserDTO> LogIn(string email, string password);
        Task<UserDTO> EditData(UpdateUserDTO updateUser);
        Task<UserDTO> DeleteUser(int userId);
        Task<UserDTO> ChangeRole(ChangeRolDTO changeRolDTO );
    }
}
