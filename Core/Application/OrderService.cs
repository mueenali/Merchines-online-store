using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;


namespace Core.Application
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, ICartRepository cartRepo, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartRepo = cartRepo;
            _mapper = mapper;
        }
        
        public async Task<Order> CreateOrderAsync(string buyerEmail, CreateOrderDto orderDto)
        {
            var cart = await _cartRepo.GetCartAsync(orderDto.CartId);
            var order = new Order();
            
            foreach (var item in cart.CartITems)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                order.AddOrderItem(product , item.Quantity);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            order.CalculateTotal();
            order.BuyerEmail = buyerEmail;
            order.DeliveryMethod = deliveryMethod;
            order.ShippingAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            await _cartRepo.DeleteCartAsync(orderDto.CartId);

            return order;
        }

        public Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderById(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}