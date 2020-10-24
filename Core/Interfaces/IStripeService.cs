using System.Threading.Tasks;
using Core.Entities;
using Stripe;

namespace Core.Interfaces
{
    public interface IStripeService
    {
        Task<PaymentIntent> CreateOrUpdatePaymentIntent(Cart cart);
    }
}