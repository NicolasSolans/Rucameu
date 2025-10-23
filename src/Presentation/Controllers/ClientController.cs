using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using System.Security.Authentication;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin, Employee, Client")]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationService _authenticationService;
        private readonly ICartService _cartService;
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
            var newClient = await _userService.RegisterClient(CreateClientDTO);
            //Creamos el carrito asociado al cliente
            var createCartDTO = new CreateCartDTO
            {
                UserId = newClient.Id,
                User = newClient,
                //Items = new List<ItemCartDTO>()
            };
            await _cartService.Create(createCartDTO);
            return newClient;
        }

        [HttpPut("/UpdateClient")]
        public async Task<ActionResult<ClientDTO>> Update([FromBody] UpdateClientDTO updateClient)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value);
            await _authenticationService.ValidateIdUser(userId, updateClient.Id);
            return await _userService.UpdateClient(updateClient);
        }
    }
}