using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _authenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {
            _authenticationService = customAuthenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationRequestDTO authenticationRequestDTO)
        {
            string newToken = await _authenticationService.Authenticate(authenticationRequestDTO);

            return newToken;
        }
    }
}