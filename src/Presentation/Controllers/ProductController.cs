using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("/AllEnableProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAllEnable()
        {
            return await _productService.GetAllEnable();
        }

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

        [HttpGet("/GetByNameEnable/{name}")]
        public async Task<ActionResult<List<ProductDTO>>> GetByNameEnable([FromRoute] string name)
        {
            return await _productService.GetByNameEnable(name);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductDTO createProductDTO)
        {
            var createProduct = await _productService.Create(createProductDTO);
            return Ok(createProduct);
        }

        [HttpPut("/UpdateProduct")]
        public async Task<ActionResult<ProductDTO>> Update([FromBody] CreateProductDTO updateProduct, int id)
        {
            var product = await _productService.Update(updateProduct, id);
            return Ok(product);
        }

        [HttpPut("/DisableProduct/{id}")]
        public async Task<ActionResult<string>> Disable([FromRoute] int id)
        {
            var product = await _productService.Disable(id);
            return Ok(product);
        }
    }
}
