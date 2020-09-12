using System.Threading.Tasks;
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
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
           return Ok( await _cartService.GetCartAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
        {
           return Ok( await _cartService.UpdateCartAsync(cart));   
        }

        [HttpDelete]
        public async Task DeleteCart(string id)
        {
           await _cartService.DeleteCartAsync(id);
        }

    }
}