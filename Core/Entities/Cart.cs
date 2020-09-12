using System.Collections.Generic;

namespace Core.Entities
{
    public class Cart
    {
        public Cart()
        {
        }

        public Cart(string id)
        {
            Id = id;
        }
        

        public string Id { get; set; }
        public List<CartItem> CartITems { get; set; } = new List<CartItem>();
    }
}