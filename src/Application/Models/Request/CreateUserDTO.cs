using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.Models.Request
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El email es obligatorio.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El numero telefonico es obligatorio.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

        public virtual User ToEntity()
        {
            var user = new User();
            user.Name = this.Name;
            user.LastName = this.LastName;
            user.Email = this.Email;
            user.PhoneNumber = this.PhoneNumber;
            user.Password = this.Password;
            user.DateRegister = DateTime.Now;
            return user;
        }
    }
}
