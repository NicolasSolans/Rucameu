using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models.Request
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre no puede estár vacío.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido no puede estár vacío.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El email no puede estár vacío.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El numero telefonico no puede estár vacío.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "La contraseña no puede estár vacía.")]
        public string Password { get; set; }

    }
} 
