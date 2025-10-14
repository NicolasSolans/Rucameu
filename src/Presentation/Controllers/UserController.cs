using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet("/LogIn")]
        //public async Task<ActionResult<UserDTO>> LogIn([FromQuery] string email, string password)
        //{
        //    return await _userService.LogIn(email, password);
        //}
       
    }
}
