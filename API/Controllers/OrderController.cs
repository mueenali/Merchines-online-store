using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.Dtos;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;
        

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto orderDto)
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var order = await _orderService.CreateOrderAsync(email, orderDto);

            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "A Problem has occured during the order creation process"));
            }

            return Ok(order);
        }
    }
}