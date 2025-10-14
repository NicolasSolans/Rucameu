using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Admin/Register")]
        public async Task<ActionResult<AdminDTO>> Create([FromBody] CreateAdminDTO createAdminDTO)
        {
            return await _userService.RegisterAdmin(createAdminDTO);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int userId)
        {
            var user = await _userService.DeleteUser(userId);
            return user;
        }
    }
}
