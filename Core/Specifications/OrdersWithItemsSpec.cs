using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsSpec : BaseSpecification<Order>
    {
        public OrdersWithItemsSpec(string userEmail) : base(o => o.BuyerEmail == userEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsSpec(int orderId, string userEmail) : 
            base(o => o.Id == orderId && o.BuyerEmail == userEmail) 
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}