using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmployeeDTO : UserDTO
    {
        public string Role { get; set; }
        public string Adress { get; set; }

        public static EmployeeDTO FromEntity(Employee employee)
        {
            if (employee == null)
                return null;

            var dto = new EmployeeDTO();
            dto.Id = employee.Id;
            dto.FullName = employee.Name + " " + employee.LastName;
            dto.Role = employee.Role;
            dto.Email = employee.Email;
            dto.PhoneNumber = employee.PhoneNumber;
            dto.Adress = employee.Adress;

            return dto;
        }
    }
}
