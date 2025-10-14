using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        //Se llama FromEntity xq representa la transformación de la clase User (entity) a un DTO
        public static UserDTO FromEntity(User user)
        {
            var dto = new UserDTO();
            dto.Id = user.Id;
            dto.FullName = user.Name + " " + user.LastName;
            dto.Email = user.Email;
            dto.PhoneNumber = user.PhoneNumber;
            dto.Role = user.Role;
            

            return dto;
        }

        public static List<UserDTO> CreateListDTO(List<User> userList)
        {
            //Completar...
            var dtoList = new List<UserDTO>();

            foreach (var p in userList)
            {
                dtoList.Add(FromEntity(p));
            }

            return dtoList;
        }

    }


}
