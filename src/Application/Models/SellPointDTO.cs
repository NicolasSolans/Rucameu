using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SellPointDTO
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Location_link { get; set; }
        public DateTime Date { get; set; }
        public string? Images { get; set; }

        public static SellPointDTO FromEntity(SellPoint sellPoint)
        {
            var dto = new SellPointDTO();
            dto.Id = sellPoint.Id;
            dto.Adress = sellPoint.Adress;
            dto.Location_link = sellPoint.Location_link;
            dto.Date = sellPoint.Date;
            dto.Images = sellPoint?.Images;


            return dto;
        }

        public static List<SellPointDTO> CreateListDTO(List<SellPoint> sellPointList)
        {
            //Completar...
            var dtoList = new List<SellPointDTO>();

            foreach (var s in sellPointList)
            {
                dtoList.Add(FromEntity(s));
            }

            return dtoList;
        }
    }
}
