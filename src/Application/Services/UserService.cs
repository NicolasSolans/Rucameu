using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepositoryBase;
        private readonly IRepositoryBase<Admin> _adminRepositoryBase;
        private readonly IRepositoryBase<Client> _clientRepositoryBase;
        private readonly IRepositoryBase<Employee> _employeeRepositoryBase;
        public UserService(IRepositoryBase<User> userRepositoryBase, IRepositoryBase<Admin> adminRepository, IRepositoryBase<Client> clientRepositoryBase, IRepositoryBase<Employee> employeeRepositoryBase)
        {
            _userRepositoryBase = userRepositoryBase;
            _adminRepositoryBase = adminRepository;
            _clientRepositoryBase = clientRepositoryBase;
            _employeeRepositoryBase = employeeRepositoryBase;
        }

        //acá directamente borras el token o lo expiras, después hay que hacerlo
        public async Task LogOut(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientUpDTO> GetClientById(int id)
        {
            var user = await _clientRepositoryBase.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("Usuario no encontrado.");
            return ClientUpDTO.FromEntity(user);
        }

        // ADMIN SERVICE
        public async Task<AdminDTO> RegisterAdmin(CreateAdminDTO createAdminDTO)
        {
            var newAdmin = createAdminDTO.ToEntity();
            await _adminRepositoryBase.CreateAsync(newAdmin);
            var newAdminDto = AdminDTO.FromEntity(newAdmin);
            return newAdminDto;
        }
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepositoryBase.GetAllAsync();
            if (users == null) throw new Exception("No se encontraron usuarios.");
            return UserDTO.CreateListDTO(users);
        }

        public async Task<object> GetAdminOrEmployeeById(int id)
        {
            var user = await _userRepositoryBase.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("Usuario no encontrado.");
            if (user.Role == "Admin")
            {
                var adminUser = await _adminRepositoryBase.GetByIdAsync(id);
                return AdminUpDTO.FromEntity(adminUser);
            } else if (user.Role == "Employee")
            {
                var employeeUser = await _employeeRepositoryBase.GetByIdAsync(id);
                return EmployeeUpDTO.FromEntity(employeeUser);
            } else
            {
                throw new Exception("El usuario encontrado es un cliente.");
            }
        }
        public async Task<AdminUpDTO> UpdateAdmin(UpdateAdminDTO updateAdmin)
        {
            var user = await _adminRepositoryBase.GetByIdAsync(updateAdmin.Id);
            if (user == null) throw new Exception("No se encontro al admin.");

            user.Name = updateAdmin.Name;
            user.LastName = updateAdmin.LastName;
            user.Email = updateAdmin.Email;
            user.PhoneNumber = updateAdmin.PhoneNumber;
            user.Adress = updateAdmin.Adress;
            //No actualiza la lista de usuarios eliminados

            await _userRepositoryBase.UpdateAsync(user);
            return AdminUpDTO.FromEntity(user);
        }
        public async Task<UserDTO> DeleteUser(int Id)
        {
            var userToDelete = await  _userRepositoryBase.GetByIdAsync(Id);
            if (userToDelete == null) throw new NotImplementedException("No se encontro al usuario.");
            if (userToDelete.Id <= 3) throw new Exception("No se puede editar un super admin.");
            // CUANDO TENGAMOS TOKEN, USAR EL ID DEL ADMIN QUE VIENE EN EL MISMO
            // _adminRepository.AddUserDeleted(userToDelete, adminId);
            await _userRepositoryBase.DeleteAsync(userToDelete);
            return UserDTO.FromEntity(userToDelete);
        }
        public async Task<UserDTO> ChangeRole(ChangeRolDTO changeRolDTO)
        {
            var findUser = await _userRepositoryBase.GetByIdAsync(changeRolDTO.Id);
            if (findUser == null) throw new NotImplementedException("No se encontro al usuario.");
            if (findUser.Id <= 3) throw new Exception("No se puede editar un super admin.");

            findUser.Id = changeRolDTO.Id;
            findUser.Role = changeRolDTO.Role;

            if (changeRolDTO.Role == "Admin")
            {
                var changedUser = new Admin
                {
                    Id = findUser.Id,
                    Name = findUser.Name,
                    LastName = findUser.LastName,
                    Email = findUser.Email,
                    Password = findUser.Password,
                    PhoneNumber = findUser.PhoneNumber,
                    Role = changeRolDTO.Role,
                    DateRegister = findUser.DateRegister,
                    Adress = changeRolDTO.Adress!
                };
                await _userRepositoryBase.DeleteAsync(findUser);
                await _userRepositoryBase.CreateAsync(changedUser);
                return UserDTO.FromEntity(changedUser);

            }
            else if (changeRolDTO.Role == "Employee")
            {
                var changedUser = new Employee
                {
                    Id = findUser.Id,
                    Name = findUser.Name,
                    LastName = findUser.LastName,
                    Email = findUser.Email,
                    Password = findUser.Password,
                    PhoneNumber = findUser.PhoneNumber,
                    Role = changeRolDTO.Role,
                    DateRegister = findUser.DateRegister,
                    Adress = changeRolDTO.Adress!
                };
                await _userRepositoryBase.DeleteAsync(findUser);
                await _userRepositoryBase.CreateAsync(changedUser);
                return UserDTO.FromEntity(changedUser);
            }
            else
            {
                throw new NotImplementedException("No se puede cambiar el rol a Client");
            }


            //await _userRepositoryBase.UpdateAsync(findUser); Esto no se puede
        }

        // EMPLOYEE SERVICE
        public async Task<EmployeeDTO> RegisterEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            var newEmployee = createEmployeeDTO.ToEntity();
            await _employeeRepositoryBase.CreateAsync(newEmployee);
            var newEmployeeDto = EmployeeDTO.FromEntity(newEmployee);
            return newEmployeeDto;
        }

        public async Task<EmployeeUpDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployee)
        {
            var user = await _employeeRepositoryBase.GetByIdAsync(updateEmployee.Id);
            if (user == null) throw new Exception("No se encontro al cliente.");

            user.Name = updateEmployee.Name;
            user.LastName = updateEmployee.LastName;
            user.Email = updateEmployee.Email;
            user.PhoneNumber = updateEmployee.PhoneNumber;
            user.Adress = updateEmployee.Adress;

            await _userRepositoryBase.UpdateAsync(user);
            return EmployeeUpDTO.FromEntity(user);
        }

        //CLIENT SERVICES
        public async Task<ClientDTO> RegisterClient(CreateClientDTO createClientDTO)
        {
            var newUser = createClientDTO.ToEntity();
            await _userRepositoryBase.CreateAsync(newUser);
            var newUserDto = ClientDTO.FromEntity(newUser);
            return newUserDto;
        }

        public async Task<ClientDTO> UpdateClient(UpdateClientDTO updateClient)
        {
            var user = await _clientRepositoryBase.GetByIdAsync(updateClient.Id);
            if (user == null) throw new Exception("No se encontro al cliente.");

            user.Name = updateClient.Name;
            user.LastName = updateClient.LastName;
            user.Email = updateClient.Email;
            user.PhoneNumber = updateClient.PhoneNumber;

            await _userRepositoryBase.UpdateAsync(user);
            return ClientDTO.FromEntity(user);
        }
    }
}
