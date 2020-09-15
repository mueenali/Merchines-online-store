using System.Threading.Tasks;
using Core.Dtos;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
    }
}