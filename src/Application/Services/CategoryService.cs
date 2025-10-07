using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryBase<Category> _repositoryBase;

        public CategoryService(IRepositoryBase<Category> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var category = await _repositoryBase.GetAllAsync();
            return CategoryDTO.CreateListDTO(category);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var findCategory = await _repositoryBase.GetByIdAsync(id);
            if (findCategory == null)
            {
                throw new Exception("No se encontro la categoria");
            }

            return CategoryDTO.CreateDTO(findCategory);
        }
        public async Task<CategoryDTO> Create(CreateCategoryDTO newCategory)
        {
            var category = new Category();

            category.Name = newCategory.Name;
            category.Description = newCategory.Description;

            await _repositoryBase.CreateAsync(category);
            return CategoryDTO.CreateDTO(category);
        }

        public async Task<CategoryDTO> Update(UpdateCategoryDTO updateCategory)
        {
            var findCategory = await _repositoryBase.GetByIdAsync(updateCategory.Id);
            //if (findCategory == null) throw new Exception("No se encontro la categoría");

            findCategory.Id = updateCategory.Id;
            findCategory.Name = updateCategory.Name;
            findCategory.Description = updateCategory.Description;

            await _repositoryBase.UpdateAsync(findCategory);
            return CategoryDTO.CreateDTO(findCategory);
        }

        //NO SE USA EN CATEGORY.
        public Task<string> Disable(int id)
        {
            throw new NotImplementedException();
        }
    }
}
