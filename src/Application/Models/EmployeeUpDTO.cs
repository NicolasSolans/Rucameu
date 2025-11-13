using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmployeeUpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public static EmployeeUpDTO FromEntity(Employee employee)
        {
            var dto = new EmployeeUpDTO();
            dto.Id = employee.Id;
            dto.Name = employee.Name;
            dto.LastName = employee.LastName;
            dto.Email = employee.Email;
            dto.PhoneNumber = employee.PhoneNumber;
            dto.Adress = employee.Adress;

            return dto;
        }
    }
}
