using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ClientUpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public static ClientUpDTO FromEntity(Client client)
        {
            var dto = new ClientUpDTO();
            dto.Id = client.Id;
            dto.Name = client.Name;
            dto.LastName = client.LastName;
            dto.Email = client.Email;
            dto.PhoneNumber = client.PhoneNumber;

            return dto;
        }
    }
}
