using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDTO
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }

        public static ProductDTO CreateDTO(Product product)
            {
                var dto = new ProductDTO();
                dto.Name = product.Name;
                dto.Description = product.Description;
                dto.ImgUrl = product.ImgUrl;
                dto.Price = product.Price;

                return dto;
            }

        public static List<ProductDTO> CreateListDTO(List<Product> productList)
        {
            //Completar...
            var dtoList = new List<ProductDTO>();

            foreach (var p in productList)
            {
                dtoList.Add(CreateDTO(p));
            }

            return dtoList;
        }
    }

}
