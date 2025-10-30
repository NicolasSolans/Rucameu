using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _queryService;
        private readonly IResendService _resendService;

        public QueryController(IQueryService queryService, IResendService resendService)
        {
            _queryService = queryService;
            _resendService = resendService;
        }

        [HttpPost("newQuery")]
        public async Task<ActionResult<QueryDTO>> CreateQuery([FromBody] CreateQueryDTO createQuery)
        {
            var result = await _queryService.CreateQuery(createQuery);
            //Envio de email
            await _resendService.Execute(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("getAllQueries")]
        public async Task<ActionResult<List<QueryDTO>>> GetAllQueries()
        {
            var result = await _queryService.GetAllQueries();
            return Ok(result);
        }

        //GETMYQUERIES

        //[Authorize(Roles = "Admin, Employee")]
        [HttpGet("getQuery/{id}")]
        public async Task<ActionResult<QueryDTO>> GetQueryById([FromRoute] int id)
        {
            var result = await _queryService.GetQueryById(id);
            return Ok(result);
        }
    }
}
