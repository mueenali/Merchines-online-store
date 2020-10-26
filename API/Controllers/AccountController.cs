
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            
            var user = await _accountService.GetCurrentUserAsync(email);
            return Ok(user);
        }

        [HttpGet("emailexists")]
        [Cached(600)]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            return await _accountService.CheckUserExistsAsync(email);
        }
        
        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            
            var userAddress =  await _accountService.GetUserAddressAsync(email);
            return Ok(userAddress);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = GetEmailFromClaims.GetEmail(HttpContext.User);
            var address = await _accountService.UpdateUserAddressAsync(addressDto, email);

            if (address == null)
            {
                return BadRequest(new ApiResponse(400, "Problem Updating the user"));
            }

            return Ok(address);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _accountService.LoginAsync(loginDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return Ok(user);
            
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _accountService.CheckUserExistsAsync(registerDto.Email))
            {
                return BadRequest(new ApiValidationError{ValidationErrors = new[]{"Email is already in use"}});
            }
            
            var user = await _accountService.RegisterAsync(registerDto);

            if (user == null)
                return BadRequest(new ApiResponse(400));

            return Ok(user);
        }

    }
}