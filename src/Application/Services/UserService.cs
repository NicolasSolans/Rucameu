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
        private readonly IRepositoryBase<Client> _clientRepositoryBase;

        
        public UserService(IRepositoryBase<User> userRepositoryBase, IRepositoryBase<Client> clientRepositoryBase)
        {
            _userRepositoryBase = userRepositoryBase;
            _clientRepositoryBase = clientRepositoryBase;
            
        }

        //acá directamente borras el token o lo expiras, después hay que hacerlo
        public async Task LogOut(int userId)
        {
            throw new NotImplementedException();
        }

        //acá despues hay que hacer todas las validaciones
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepositoryBase.GetAllAsync();
            return UserDTO.CreateListDTO(users);
            
        }
        public async Task<UserDTO> Register(CreateUserDTO createUserDTO)
        {
            var newUser = createUserDTO.ToEntity();
            await _userRepositoryBase.CreateAsync(newUser);
            var newUserDto = UserDTO.FromEntity(newUser);
            return newUserDto;
        }

        public async Task<UserDTO> LogIn(string email, string password)
        {
            var users = await _userRepositoryBase.GetAllAsync();
            var userCheck = users.FirstOrDefault(u => u.Email == email && u.Password == password) ;
            if (userCheck == null) 
            {
                throw new NotFoundException("email o contraseña invalido");
            }
            return UserDTO.FromEntity(userCheck);
        }

        public async Task<UserDTO> EditData(UpdateUserDTO updateUserDTO)
        {
            var findUser = await _userRepositoryBase.GetByIdAsync(updateUserDTO.Id);
            
            findUser.Id = updateUserDTO.Id;
            findUser.Name = updateUserDTO.Name;
            findUser.LastName = updateUserDTO.LastName;
            findUser.Email = updateUserDTO.Email;
            findUser.PhoneNumber = updateUserDTO.PhoneNumber;
            findUser.Password = updateUserDTO.Password;

            await _userRepositoryBase.UpdateAsync(findUser);
            return UserDTO.FromEntity(findUser);
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
            user.Password = updateClient.Password;

            await _userRepositoryBase.UpdateAsync(user);
            return ClientDTO.FromEntity(user);
        }
    }
}
