using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : User
    {
        public Admin() 
        {
            Role = "Admin";
        }
        public string Adress {  get; set; }
        public ICollection<User>? UsersDeleted { get; set; } = new List<User>();
    }
}
