using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Exceptions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICustomAuthenticationService _authenticationService;

        public ItemCartController(ICartService cartService, ICustomAuthenticationService authenticationService)
        {
            _cartService = cartService;
            _authenticationService = authenticationService;
        }
        [HttpPost("AgregarItem")]
        public async Task<ActionResult<ItemCartDTO>> AddProductToCart(CreateItemCartDTO createItemCartDTO)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value);
            var Cart = await _cartService.GetById(createItemCartDTO.CartId);
            if (Cart == null) throw new Exception("No se encontró el carrito");
            await _authenticationService.ValidateIdUser(userId, Cart.UserId);
            return await _cartService.AddItemCart(createItemCartDTO);
        }

    }
}
