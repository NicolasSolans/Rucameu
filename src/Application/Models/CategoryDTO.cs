using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static CategoryDTO CreateDTO(Category category)
        {
            var dto = new CategoryDTO();
            dto.Name = category.Name;
            dto.Description = category.Description;

            return dto;
        }

        public static List<CategoryDTO> CreateListDTO(List<Category> category)
        {
            var dtoList = new List<CategoryDTO>();
            foreach (var c in category)
            {
                dtoList.Add(CreateDTO(c));
            }

            return dtoList;
        }
    }
}
