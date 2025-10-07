using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public static UpdateCategoryDTO FromEntity(Category category)
        //{
        //    var dto = new UpdateCategoryDTO();
        //    dto.Id = category.Id;
        //    dto.Name = category.Name;
        //    dto.Description = category.Description;

        //    return dto;
        //}
    }
}
