using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;


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
        
        public async Task<OrderDto> CreateOrderAsync(string buyerEmail, CreateOrderDto orderDto)
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

            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsSpec(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderById(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsSpec(id, buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetEntityWtihSpecAsync(spec);
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }
    }
}