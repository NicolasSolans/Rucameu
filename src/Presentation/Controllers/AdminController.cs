using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationService _authenticationService;

        public AdminController(IUserService userService, ICustomAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("/RegisterAdmin")]
        public async Task<ActionResult<AdminDTO>> Create([FromBody] CreateAdminDTO createAdminDTO)
        {
            return await _userService.RegisterAdmin(createAdminDTO);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("/DeleteUser/{userId}")]
        public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] int userId)
        {
            var user = await _userService.DeleteUser(userId);
            return user;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("/UpdateAdmin")]
        public async Task<ActionResult<AdminUpDTO>> Update([FromBody] UpdateAdminDTO updateAdmin)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value);
            await _authenticationService.ValidateIdUser(userId, updateAdmin.Id);
            return await _userService.UpdateAdmin(updateAdmin);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/ChangeRole")]
        public async Task<ActionResult<UserDTO>> ChangeRole([FromBody] ChangeRolDTO changeRolDTO)
        {
            return await _userService.ChangeRole(changeRolDTO);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("/GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        //[HttpGet("/GetAdminById/{id}")]
        //public async Task<ActionResult<AdminUpDTO>> GetAdminById([FromRoute] int id)
        //{
        //    return Ok(await _userService.GetAdminById(id));
        //}
    }
}
