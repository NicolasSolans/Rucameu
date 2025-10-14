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
    public class CreateAdminDTO : CreateUserDTO
    {
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Adress { get; set; }

        public virtual Admin ToEntity()
        {
            var admin = new Admin();
            admin.Name = this.Name;
            admin.LastName = this.LastName;
            admin.Email = this.Email;
            admin.PhoneNumber = this.PhoneNumber;
            admin.Password = this.Password;
            admin.DateRegister = DateTime.Now;
            admin.Adress = this.Adress;
            admin.UsersDeleted = new List<User>(); // inicializamos la lista vacía

            return admin;
        }
    }
}
