using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderbyPaymentIntentIdSpec : BaseSpecification<Order>
    {
        public OrderbyPaymentIntentIdSpec(string paymentIntentId) :
            base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}