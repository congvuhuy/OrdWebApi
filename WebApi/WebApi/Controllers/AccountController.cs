using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.DTOs;
using WebApi.Services.UserService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")] public async Task<IActionResult> Register([FromBody] UserRegisterDTO model) { 
            var result = await _userService.RegisterAsync(model);
            if (result.Succeeded) { 
                return Ok(result); 
            } 
            return BadRequest(result.Errors); 
        }
        [HttpPost("login")] public async Task<IActionResult> Login([FromBody] UserLoginDTO model) { 
            var token = await _userService.LoginAsync(model); 
            if (token != null) { 
                return Ok(new { token }); 
            } 
            return Unauthorized("Đăng nhập không thành công"); 
        }
    }
}
