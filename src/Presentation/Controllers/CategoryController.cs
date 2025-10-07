using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpGet("/AllCategory")]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet("/GetByIdd/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById([FromRoute] int id)
        {
            return await _categoryService.GetById(id);
        }

        [HttpPost("/CreateCategory")]
        public async Task<ActionResult<CategoryDTO>> Create(CreateCategoryDTO newCategory)
        {
            return await _categoryService.Create(newCategory);
        }

        [HttpPut("/UpdateCategory")]
        public async Task<ActionResult<CategoryDTO>> Update([FromBody]UpdateCategoryDTO updateCategory)
        {
            return await _categoryService.Update(updateCategory);
        }
    }
}
