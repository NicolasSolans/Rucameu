using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SellPointCalenderDTO
    {
        public DateTime Date {  get; set; }
        public string Adress { get; set; }
        public string Location_Link { get; set; }
        public string? Images { get; set; }

        public static SellPointCalenderDTO FromEntity(SellPoint sellPoint)
        {
            return new SellPointCalenderDTO
            {
                Date = sellPoint.Date,
                Adress = sellPoint.Adress,
                Location_Link = sellPoint.Location_link
            };
        }

        public static List<SellPointCalenderDTO> CreateListDTO(List<SellPoint> sellPoint)
        {
            var dtoList = new List<SellPointCalenderDTO>();
            foreach (var c in sellPoint)
            {
                dtoList.Add(FromEntity(c));
            }

            return dtoList;
        }
    }
}
