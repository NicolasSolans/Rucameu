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
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public QueryController(IQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpPost("newQuery")]
        public async Task<ActionResult<QueryDTO>> CreateQuery([FromBody] CreateQueryDTO createQuery)
        {
            var result = await _queryService.CreateQuery(createQuery);
            return Ok(result);
        }

        [HttpGet("getAllQueries")]
        public async Task<ActionResult<List<QueryDTO>>> GetAllQueries()
        {
            var result = await _queryService.GetAllQueries();
            return Ok(result);
        }

        [HttpGet("getQuery/{id}")]
        public async Task<ActionResult<QueryDTO>> GetQueryById([FromRoute] int id)
        {
            var result = await _queryService.GetQueryById(id);
            return Ok(result);
        }
    }
}
