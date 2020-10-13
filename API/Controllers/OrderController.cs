using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.Dtos;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetUserOrders()
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
        public async Task<ActionResult<Order>> GetUserOrderById(int orderId)
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var order = await _orderService.GetOrderById(orderId, email);
            
            if (order == null)
                return NotFound(new ApiResponse(404));
            
            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}