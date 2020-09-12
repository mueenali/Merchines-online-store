using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Core.Application {
    public class CartService : ICartService {
        private readonly ICartRepository _cartRepo;
        public CartService (ICartRepository cartRepo) {
            _cartRepo = cartRepo;
        }

        public async Task<bool> DeleteCartAsync (string cartId) {
            var deleted = await _cartRepo.DeleteCartAsync(cartId);
            
            return deleted;
        }

        public async Task<Cart> GetCartAsync (string cartId) {
            var cart = await _cartRepo.GetCartAsync(cartId);

            return cart ?? new Cart(cartId);
        }

        public async Task<Cart> UpdateCartAsync (Cart cart) {
            var updatedCart = await _cartRepo.UpdateCartAsync(cart);

            return updatedCart;
        }
    }
}