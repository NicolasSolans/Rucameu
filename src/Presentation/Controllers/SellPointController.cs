using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Employee")]
    public class SellPointController : ControllerBase
    {
        private readonly ISellPointService _sellPointService;
        public SellPointController(ISellPointService sellPointService)
        {
            _sellPointService = sellPointService;
        }

        [HttpPost("/Create")]
        public async Task<ActionResult<SellPointDTO>> CreateSellPoint([FromBody] CreateSellPointDTO createSellPointDTO)
        {
            return await _sellPointService.CreateSellPoint(createSellPointDTO);

        }

        [HttpDelete("/Delete/{id}")]
        public async Task<ActionResult> DeleteSellPoint([FromRoute]int sellPointId)
        {
            await _sellPointService.DeleteSellPoint(sellPointId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("/GettAll")]
        public async Task<ActionResult<List<SellPointDTO>>> GetAllSellPoints()
        {
            return await _sellPointService.GetAllSellPoints();
        }

        [AllowAnonymous]
        [HttpGet("/GetByAddress/{address}")]
        public async Task<List<SellPointDTO>> GetSellPointByAddress([FromRoute] string adress)
        {
            return await _sellPointService.GetSellPointByAdress(adress);
        }

        [HttpPut("/UpdateSellPoint")]
        public async Task<SellPointDTO> UpdateSellPoint([FromBody] UpdateSellPointDTO updateSellPointDTO)
        {
            return await _sellPointService.UpdateSellPoint(updateSellPointDTO);
        }

        [AllowAnonymous]
        [HttpGet("/Calender")]
        public async Task<List<SellPointCalenderDTO>> GetSellPointCalender([FromQuery] DateTime start, DateTime end)
        {
            return await _sellPointService.GetSellPointsInDateRange(start, end);
        }
    }
     
}
