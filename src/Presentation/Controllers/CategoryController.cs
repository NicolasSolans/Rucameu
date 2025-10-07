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

        [HttpGet("/GetAllCategory")]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            return await _categoryService.GetAll();
        }

        [HttpPost("/CreateCategory")]
        public async Task<ActionResult<CategoryDTO>> Create(CreateCategoryDTO newCategory)
        {
            return await _categoryService.Create(newCategory);
        }
    }
}
