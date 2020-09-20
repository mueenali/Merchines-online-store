using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> GetCurrentUserAsync(string email);
        Task<bool> CheckUserExistsAsync(string email);
        Task<AddressDto> GetUserAddressAsync(string email);
        Task<AddressDto> UpdateUserAddressAsync(AddressDto addressDto, string email);
    }
}