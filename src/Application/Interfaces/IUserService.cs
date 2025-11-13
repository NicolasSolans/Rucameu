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
        Task LogOut(int userId);
        //Client Service
        Task<ClientDTO> RegisterClient(CreateClientDTO createClientDTO);
        Task<ClientDTO> UpdateClient(UpdateClientDTO updateClient);
        Task<object> GetAdminOrEmployeeById(int id);

        Task<ClientUpDTO> GetClientById(int id);
        //Admin Service
        Task<UserDTO> ChangeRole(ChangeRolDTO changeRolDTO);
        Task<AdminUpDTO> UpdateAdmin(UpdateAdminDTO updateAdmin);
        Task<AdminDTO> RegisterAdmin(CreateAdminDTO createAdminDTO);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> DeleteUser(int userId);
        //Employee Service
        Task<EmployeeDTO> RegisterEmployee(CreateEmployeeDTO createEmployeeDTO);
        Task<EmployeeUpDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployee);

    }
}
