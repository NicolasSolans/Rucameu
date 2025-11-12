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

        Task<ClientUpDTO> GetClientById(int id);
        //Admin Service
        Task<UserDTO> ChangeRole(ChangeRolDTO changeRolDTO);
        Task<AdminDTO> UpdateAdmin(UpdateAdminDTO updateAdmin);
        Task<AdminDTO> RegisterAdmin(CreateAdminDTO createAdminDTO);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> DeleteUser(int userId);
        //Employee Service
        Task<EmployeeDTO> RegisterEmployee(CreateEmployeeDTO createEmployeeDTO);
        Task<EmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployee);

    }
}
