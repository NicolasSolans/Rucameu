using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateSellPointDTO
    {
        public string Adress { get; set; }
        public string Location_link { get; set; }
        public DateTime Date { get; set; }
        public string? Images { get; set; }

        public virtual SellPoint ToEntity()
        {
            var sellPoint = new SellPoint();
            sellPoint.Adress = this.Adress;
            sellPoint.Location_link = this.Location_link;
            sellPoint.Date = this.Date;
            sellPoint.Images = this.Images;

            return sellPoint;
        }
    }
}
