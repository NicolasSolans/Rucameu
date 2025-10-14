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
        [HttpGet("/GetAllUsers")]
        public async Task<List<UserDTO>> GetAll()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost("/Register")]
        public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO createUserDTO)
        {
            return await _userService.Register(createUserDTO);
        }

        [HttpPut("/UpdateUser")]
        public async Task<ActionResult<UserDTO>> Update([FromBody] UpdateUserDTO updateUser)
        {
            return await _userService.EditData(updateUser);
        }

        [HttpGet("/LogIn")]
        public async Task<ActionResult<UserDTO>> LogIn([FromQuery] string email, string password)
        {
            return await _userService.LogIn(email, password);
        }
       
    }
}
