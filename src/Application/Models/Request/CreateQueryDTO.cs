using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateQueryDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Message { get; set; }
        public int CartId { get; set; }

         public virtual Query ToEntity(Cart cart)
         {
               var query = new Query();
               query.Message = Message;
               query.DateConsult = DateTime.Now;
               query.CartId = CartId;
               query.Cart = cart;
                //query.UserId = this.User;

               return query;
         }
    }
}

