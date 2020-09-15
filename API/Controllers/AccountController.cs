using System.Threading.Tasks;
using API.Errors;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using  Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IAccountService _accountService;

        // GET
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _accountService.Login(loginDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return Ok(user);
            
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _accountService.Register(registerDto);

            if (user == null)
                return BadRequest(new ApiResponse(400));

            return Ok(user);
        }
    }
}