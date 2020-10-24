using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;


namespace Core.Application
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public PaymentService(ICartRepository cartRepo, IUnitOfWork unitOfWork, 
            IStripeService stripeService, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _unitOfWork = unitOfWork;
            _stripeService = stripeService;
            _mapper = mapper;
        }
        public async Task<CartDto> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await _cartRepo.GetCartAsync(cartId);
            if (cart == null) return null;
            
            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork
                    .Repository<DeliveryMethod>().GetByIdAsync((int)cart.DeliveryMethodId);
                cart.ShippingPrice = deliveryMethod.Price;
                
            }

            foreach (var item in cart.CartITems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var intent = await _stripeService.CreateOrUpdatePaymentIntent(cart);
            if (intent != null)
            {
                cart.PaymentIntent = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            }

            await _cartRepo.UpdateCartAsync(cart);
            return _mapper.Map<Cart, CartDto>(cart);
        }

        public async Task<Order> UpdateOrderPaymentStatus(string paymentIntentId, bool status)
        {
            var spec = new OrderbyPaymentIntentIdSpec(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWtihSpecAsync(spec);
            if (order == null) return null;

            switch (status)
            {
                case true :
                    order.OrderStatus = OrderStatus.PaymentReceived;
                    _unitOfWork.Repository<Order>().Update(order);
                    break;
                case false:
                    order.OrderStatus = OrderStatus.PaymentFailed;
                    _unitOfWork.Repository<Order>().Update(order);
                    break;
            }

            await _unitOfWork.Complete();
            return order;
        }
    }
} 