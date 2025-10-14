using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User
    {

        // Constructor con los parámetros necesarios, llamando al constructor de User
        public Client()
        {
            Role = "Client";
        }

        //Entidad carrito
        //public Cart Cart { get; set; };
    }
}
