using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return TotalPrice + DeliveryMethod.Price;
        }

        public void AddOrderItem(Product product, int quantity)
        {
            var itemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, product.Price,quantity);
            OrderItems.Add(orderItem);
        }

        public void CalculateTotal()
        {
            var total = OrderItems.Sum(item => item.Price * item.Quantity);
            this.TotalPrice = total;
        }
    }
}