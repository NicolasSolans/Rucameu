using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _userService;
        public ClientController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/RegisterClient")]
        public async Task<ActionResult<ClientDTO>> Create([FromBody] CreateClientDTO CreateClientDTO)
        {
            return await _userService.RegisterClient(CreateClientDTO);
        }

        [HttpPut("/UpdateClient")]
        public async Task<ActionResult<ClientDTO>> Update([FromBody] UpdateClientDTO updateClient)
        {
            return await _userService.UpdateClient(updateClient);
        }
    }
}
