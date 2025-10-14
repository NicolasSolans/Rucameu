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

        [HttpPost("/Register")]
        public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO createUserDTO)
        {
            return await _userService.Register(createUserDTO);
        }
        //usar JWT para validar que el id del mismo sea el mismo que viene en la request.
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

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int userId)
        {
            var user = await _userService.DeleteUser(userId);
            return user;
        }

    }
}
