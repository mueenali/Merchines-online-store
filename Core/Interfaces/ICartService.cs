using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartService
    {
         Task<CartDto> GetCartAsync(string cartId);
         Task<CartDto> UpdateCartAsync(CartDto cart);
         Task<bool> DeleteCartAsync(string cartId);
    }
}