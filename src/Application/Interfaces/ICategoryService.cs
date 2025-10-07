using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);

        Task<CategoryDTO> Create(CreateCategoryDTO newCategory);
        Task<CategoryDTO> Update(UpdateCategoryDTO updateCategory);
        Task<string> Disable(int id);
            
    }
}
