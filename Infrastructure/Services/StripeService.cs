using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class StripeService: IStripeService
    {
        private readonly IConfiguration _config;

        public StripeService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Cart cart)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            
            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(cart.PaymentIntent))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long) cart.CartITems.Sum(i =>
                        i.Quantity * (i.Price * 100)) + (long) cart.ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> {"card"}
                };
                
                var intent = await service.CreateAsync(options);
                return intent;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) cart.CartITems.Sum(i =>
                        i.Quantity * (i.Price * 100)) + (long) cart.ShippingPrice * 100,
                };
                
                await service.UpdateAsync(cart.PaymentIntent ,options);
            }

            return null;
        }
    }
}