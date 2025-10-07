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

        public Task<CategoryDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<CategoryDTO> Create(CreateCategoryDTO newCategory)
        {
            var category = new Category();

            category.Name = newCategory.Name;
            category.Description = newCategory.Description;

            await _repositoryBase.CreateAsync(category);
            return CategoryDTO.CreateDTO(category);
        }

        public Task<CategoryDTO> Update(CreateCategoryDTO updateCategory)
        {
            throw new NotImplementedException();
        }
        public Task<string> Disable(int id)
        {
            throw new NotImplementedException();
        }
    }
}
