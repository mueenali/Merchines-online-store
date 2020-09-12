using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            var cartData = await _database.StringGetAsync(cartId);
            
            return cartData.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(cartData);
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            var serializedCart =  JsonSerializer.Serialize(cart);
            var created = await _database.StringSetAsync(cart.Id, serializedCart,
                TimeSpan.FromDays(20));
            
            if(!created) 
                return null;
            
            return await GetCartAsync(cart.Id);
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            var deleted = await _database.KeyDeleteAsync(cartId);
            
            return deleted;
        }
    }
}