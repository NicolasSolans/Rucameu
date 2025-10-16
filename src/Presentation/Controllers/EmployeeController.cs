using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin, Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Employee/Register")]
        public async Task<ActionResult<EmployeeDTO>> Create([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            return await _userService.RegisterEmployee(createEmployeeDTO);
        }

        [HttpPut("/UpdateEmployee")]
        public async Task<ActionResult<EmployeeDTO>> Update([FromBody] UpdateEmployeeDTO updateEmployee)
        {
            return await _userService.UpdateEmployee(updateEmployee);
        }
    }
}
