using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using System.Security.Authentication;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin, Employee, Client")]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationService _authenticationService;
        public ClientController(IUserService userService, ICustomAuthenticationService customAuthenticationService)
        {
            _userService = userService;
            _authenticationService = customAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("/LogIn")]
        //RUTA GENERICA PARA TODOS LOS USERS.
        public async Task<ActionResult<string>> LogIn([FromBody] AuthenticationRequestDTO authenticationRequestDTO)
        {
            string newToken = await _authenticationService.Authenticate(authenticationRequestDTO);

            return newToken;
        }

        [AllowAnonymous]
        [HttpPost("/RegisterClient")]
        public async Task<ActionResult<ClientDTO>> Create([FromBody] CreateClientDTO CreateClientDTO)
        {
            return await _userService.RegisterClient(CreateClientDTO);
        }

        [HttpPut("/UpdateClient")]
        public async Task<ActionResult<ClientDTO>> Update([FromBody] UpdateClientDTO updateClient)
        {
            var userId = int.Parse(User.FindFirst("sub")?.Value);
            await _authenticationService.ValidateIdUser(userId, updateClient.Id);
            return await _userService.UpdateClient(updateClient);
        }
    }
}