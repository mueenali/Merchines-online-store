using System.IO;
using System.Threading.Tasks;
using API.Errors;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentController : ApiBaseController
    {
        private const string WhSecret = "";
        private readonly IPaymentService _paymentService;
        private readonly ILogger<IPaymentService> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<IPaymentService> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await _paymentService.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null) return BadRequest(new ApiResponse(400, "Your Cart does not exist"));

            return Ok(cart);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, 
                Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Order order;
            switch (stripeEvent.Type)
            {
                case "payment_intent_succeeded":
                    intent = (PaymentIntent) stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentStatus(intent.Id, true);
                    _logger.LogInformation("Order status updated to payment received", order.Id);
                    break;
                case "payment_intent_failed":
                    intent = (PaymentIntent) stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentStatus(intent.Id, false);
                    _logger.LogInformation("Payment failed", order.Id);
                    break;
            }
            
            return new EmptyResult();
        }
    }
}