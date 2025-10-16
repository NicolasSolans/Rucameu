using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateEmployeeDTO : CreateUserDTO
    {
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Adress { get; set; }

        public virtual Employee ToEntity()
        {
            var employee = new Employee();
            employee.Name = this.Name;
            employee.LastName = this.LastName;
            employee.Email = this.Email;
            employee.PhoneNumber = this.PhoneNumber;
            employee.Password = this.Password;
            employee.DateRegister = DateTime.Now;
            employee.Adress = this.Adress;

            return employee;
        }
    }
}
