using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.OrderAggregate;


namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CartDto> CreateOrUpdatePaymentIntent(string cartId);
        Task<Order> UpdateOrderPaymentStatus(string paymentIntentId, bool status);
    }
}