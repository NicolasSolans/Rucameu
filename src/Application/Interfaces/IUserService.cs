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
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> Register(CreateUserDTO createUserDTO);
        Task<AdminDTO> RegisterAdmin(CreateAdminDTO createAdminDTO);
        Task<ClientDTO> RegisterClient(CreateClientDTO createClientDTO);
        Task<EmployeeDTO> RegisterEmployee(CreateEmployeeDTO createEmployeeDTO);
        Task LogOut(int userId);
        Task<UserDTO> LogIn(string email, string password);
        Task<UserDTO> EditData(UpdateUserDTO updateUser);
        Task<UserDTO> DeleteUser(int userId);
        Task<UserDTO> ChangeRole(ChangeRolDTO changeRolDTO );
        Task<ClientDTO> UpdateClient(UpdateClientDTO updateClient);
        Task<EmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployee);
        Task<AdminDTO> UpdateAdmin(UpdateAdminDTO updateAdmin);



    }
}
