
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            UserFactory userFactory , ITokenService tokenService, IMapper mapper )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userFactory = userFactory;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            return !result.Succeeded ? null
                : _userFactory.CreateUserDto(user, _tokenService.GenerateToken(user));
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = _userFactory.Create(registerDto.Email, registerDto.DisplayName);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            return !result.Succeeded ? null 
                : _userFactory.CreateUserDto(user, _tokenService.GenerateToken(user));
        }

        public async Task<UserDto> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _userFactory.CreateUserDto(user, _tokenService.GenerateToken(user));
        }

        public async Task<bool> CheckUserExists(string email)
        {
            var result = await _userManager.FindByEmailAsync(email) != null;
            return result;
        }

        public async Task<AddressDto> GetUserAddress(string email)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(email);
            
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string email)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(email);
            user.Address = _mapper.Map<AddressDto, Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);

            return !result.Succeeded ? null : _mapper.Map<Address, AddressDto>(user.Address);
        }
    }
}