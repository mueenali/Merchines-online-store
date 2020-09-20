using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;

namespace Core.Application {
    public class CartService : ICartService {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepo;
        public CartService (ICartRepository cartRepo , IMapper mapper)
        {
            _mapper = mapper;
            _cartRepo = cartRepo;
        }

        public async Task<bool> DeleteCartAsync (string cartId) {
            var deleted = await _cartRepo.DeleteCartAsync(cartId);
            
            return deleted;
        }

        public async Task<CartDto> GetCartAsync (string cartId) {
            var cart = await _cartRepo.GetCartAsync(cartId);

            var mappedCart = _mapper.Map<Cart, CartDto>(cart);

            return mappedCart ?? _mapper.Map<Cart, CartDto>(new Cart(cartId));
        }

        public async Task<CartDto> UpdateCartAsync (CartDto cartDto)
        {
            var cart = _mapper.Map<CartDto, Cart>(cartDto);
            var updatedCart = await _cartRepo.UpdateCartAsync(cart);
            var mappedCart = _mapper.Map<Cart, CartDto>(updatedCart);
            
            return mappedCart ;
        }
    }
}