using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, CreateOrderDto orderDto);

        Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail);
        Task<Order> GetOrderById(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}