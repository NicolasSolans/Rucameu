using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin, Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthenticationService _authenticationService;

        public EmployeeController(IUserService userService, ICustomAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpPost("/Employee/Register")]
        public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            return await _userService.RegisterEmployee(createEmployeeDTO);
        }

        [HttpPut("/UpdateEmployee")]
        public async Task<ActionResult<EmployeeDTO>> Update([FromBody] UpdateEmployeeDTO updateEmployee)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value);
            await _authenticationService.ValidateIdUser(userId, updateEmployee.Id);
            return await _userService.UpdateEmployee(updateEmployee);
        }
    }
}
