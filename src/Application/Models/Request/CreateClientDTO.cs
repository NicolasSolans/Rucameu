using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.Models.Request
{
    public class CreateClientDTO : CreateUserDTO
    {
        //Al heredarse, ya estan los atributos con sus reestricciones.

        //ToEntity => viene del front a la base de datos. 
        public virtual Client ToEntity()
        {
            var client = new Client();

            client.Name = this.Name;
            client.LastName = this.LastName;
            client.Email = this.Email;
            client.PhoneNumber = this.PhoneNumber;
            client.Password = this.Password;

            return client;
        }
    }
}
