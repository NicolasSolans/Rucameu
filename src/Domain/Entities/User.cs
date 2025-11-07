using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public DateTime DateRegister { get; set; } = DateTime.Now;
        public List<Cart> Carts { get; set; } = new List<Cart>(); //Se inicializa en vacío por si el usuario no tiene ninguno (ejemplo Admin)

        public User(string name, string lastName, string email, string password, string phoneNumber)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        // Para comunicarte con la base de datos, ya que entity framework por cada request crea una
        // instancia de nuestra clase vacía, ya que efCore solo sabe que datos tiene que rellenar, pero no donde
        public User() { }
    }
}
