using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AdminUpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public static AdminUpDTO FromEntity(Admin admin)
        {
            var dto = new AdminUpDTO();
            dto.Id = admin.Id;
            dto.Name = admin.Name;
            dto.LastName = admin.LastName;
            dto.Email = admin.Email;
            dto.PhoneNumber = admin.PhoneNumber;
            dto.Adress = admin.Adress;

            return dto;
        }
    }
}
