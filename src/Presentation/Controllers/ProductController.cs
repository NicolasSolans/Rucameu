using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Employee")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpGet("/AllProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            return await _productService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("/AllEnableProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAllEnable()
        {
            return await _productService.GetAllEnable();
        }

        [AllowAnonymous]
        [HttpGet("/GetById/{id}")]
        public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
        {
            return await _productService.GetById(id);
        }

        [HttpGet("/GetByName/{name}")]
        public async Task<ActionResult<List<ProductDTO>>> GetByName([FromRoute] string name)
        {
            return await _productService.GetByName(name);
        }

        [AllowAnonymous]
        [HttpGet("/GetByNameEnable/{name}")]
        public async Task<ActionResult<List<ProductDTO>>> GetByNameEnable([FromRoute] string name)
        {
            return await _productService.GetByNameEnable(name);
        }

        [HttpPost("/CreateProduct")]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductDTO createProductDTO)
        {
            var createProduct = await _productService.Create(createProductDTO);
            return Ok(createProduct);
        }

        [HttpPut("/UpdateProduct")]
        public async Task<ActionResult<ProductDTO>> Update([FromBody] UpdateProductDTO updateProduct)
        {
            var product = await _productService.Update(updateProduct);
            return Ok(product);
        }

        [HttpPut("/EnableProduct/{id}")]
        public async Task<ActionResult<string>> Enable([FromRoute] int id)
        {
            var product = await _productService.Enable(id);
            return Ok(product);
        }

        [HttpPut("/DisableProduct/{id}")]
        public async Task<ActionResult<string>> Disable([FromRoute] int id)
        {
            var product = await _productService.Disable(id);
            return Ok(product);
        }

        [HttpDelete("/DeleteProduct/{id}")]
        public async Task<ActionResult<string>> Delete([FromRoute] int id)
        {
            var product = await _productService.Delete(id);
            return Ok(product);
        }
    }
}
