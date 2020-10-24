using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class CartDto
    {
        [Required]
        public string Id { get; set; }
        public List<CartItemDto> CartITems { get; set; }
        public decimal ShippingPrice { get; set; }
        public int DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntent { get; set; }
    }
}