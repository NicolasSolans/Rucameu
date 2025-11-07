using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class QueryDTO //Para admin por el momento.
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateConsult { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
        public CartDTO Cart { get; set; }


        public static QueryDTO FromEntity(Query query)
        {
            var dto = new QueryDTO();
            dto.Id = query.Id;
            dto.Message = query.Message;
            dto.DateConsult = query.DateConsult;
            dto.Status = query.Status;
            dto.Cart = CartDTO.FromEntity(query.Cart);

            return dto;
        }

        public static List<QueryDTO> CreateListDTO(List<Query> queryList)
        {
            var dtoList = new List<QueryDTO>();
            foreach (var q in queryList)
            {
                dtoList.Add(FromEntity(q));
            }
            return dtoList;
        }

    }
}
