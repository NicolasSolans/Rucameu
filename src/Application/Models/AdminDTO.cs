using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AdminDTO : UserDTO
    {
        public string Role { get; set; } 
        public string Adress { get; set; }
        public ICollection<User>? UsersDeleted { get; set; } = new List<User>();
    

        public static AdminDTO FromEntity(Admin admin)
        {
            if (admin == null)
                return null;

            var dto = new AdminDTO();
            dto.Id = admin.Id;
            dto.FullName = admin.Name + " " + admin.LastName;
            dto.Role = admin.Role;
            dto.Email = admin.Email;
            dto.PhoneNumber = admin.PhoneNumber;
            dto.Adress = admin.Adress;
            dto.UsersDeleted = admin.UsersDeleted;


            return dto;
        }
    }
}
