using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;
using Domain.Exeptions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepositoryBase;
        public UserService(IRepositoryBase<User> userRepositoryBase)
        {
            _userRepositoryBase = userRepositoryBase;
        }

        //acá directamente borras el token o lo expiras, después hay que hacerlo
        public async Task LogOut(int userId)
        {
            throw new NotImplementedException();
        }

        //acá despues hay que hacer todas las validaciones
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
    }
}
