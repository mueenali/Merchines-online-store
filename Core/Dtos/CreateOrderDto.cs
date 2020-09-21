namespace Core.Dtos
{
    public class CreateOrderDto
    {
        public string CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}