using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers {
    public class CartController : ApiBaseController {
        private readonly ICartService _cartService;
        public CartController (ICartService cartService) {
            _cartService = cartService;

        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart(string id)
        {
           return Ok( await _cartService.GetCartAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> UpdateCart(CartDto cartDto)
        {
           return Ok( await _cartService.UpdateCartAsync(cartDto));   
        }

        [HttpDelete]
        public async Task DeleteCart(string id)
        {
           await _cartService.DeleteCartAsync(id);
        }

    }
}