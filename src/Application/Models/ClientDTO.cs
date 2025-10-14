using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ClientDTO : UserDTO
    {
        public static ClientDTO FromEntity(Client client)
        {
            var dto = new ClientDTO();
            dto.Id = client.Id;
            dto.FullName = client.Name + " " + client.LastName;
            dto.Email = client.Email;
            dto.PhoneNumber = client.PhoneNumber;
            dto.Role = client.Role;

            return dto;
        }
    }
}
