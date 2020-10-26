using System.Collections.Generic;
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
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var order = await _orderService.CreateOrderAsync(email, orderDto);

            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "A Problem has occured during the order creation process"));
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetUserOrders()
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var orders = await _orderService.GetUserOrdersAsync(email);

            if (orders == null)
            {
                return BadRequest(new ApiResponse(400, "The user has not ordered anything yet "));
                
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Cached(300)]
        public async Task<ActionResult<OrderDto>> GetUserOrderById(int id)
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var order = await _orderService.GetOrderById(id, email);
            
            if (order == null)
                return NotFound(new ApiResponse(404));
            
            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        [Cached(300)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}