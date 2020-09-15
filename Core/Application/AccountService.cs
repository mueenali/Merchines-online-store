using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities.Identity;
using Core.Factories;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserFactory _userFactory;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            UserFactory userFactory , ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userFactory = userFactory;
            _tokenService = tokenService;
        }
        
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            return !result.Succeeded
                ? null 
                : new UserDto
                {
                    Email = user.Email, 
                    DisplayName = user.DisplayName, 
                    Token = _tokenService.GenerateToken(user)
                };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = _userFactory.Create(registerDto.Email, registerDto.DisplayName);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            return !result.Succeeded
                ? null
                : new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenService.GenerateToken(user),
                    Email = user.Email
                };
        }
    }
}