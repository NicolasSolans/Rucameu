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
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Enable { get; set; }

        public int? CategoryId { get; set; }

        public CategoryDTO CategoryDTO { get; set; }

        public static ProductDTO CreateDTO(Product product)
            {
                var dto = new ProductDTO();
                dto.Id = product.Id;
                dto.Name = product.Name;
                dto.Description = product.Description;
                dto.ImgUrl = product.ImgUrl;
                dto.Price = product.Price;
                dto.Stock = product.Stock;
                dto.Enable = product.Enable;
                dto.CategoryDTO = CategoryDTO.FromEntity(product.Category);

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
