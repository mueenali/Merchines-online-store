using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string buyerEmail, CreateOrderDto orderDto);

        Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(string buyerEmail);
        Task<OrderDto> GetOrderById(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}