using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> GetCurrentUser(string email);
        Task<bool> CheckUserExists(string email);
        Task<AddressDto> GetUserAddress(string email);
        Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string email);
    }
}