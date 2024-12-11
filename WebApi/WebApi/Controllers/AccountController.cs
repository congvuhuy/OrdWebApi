using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager=roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register( UserRegisterDTO model)
        {
            var user = new ApplicationUser
            {
               
                UserName = model.Email,
                FullName = model.FullName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            var userResult= await _userManager.CreateAsync(user,model.Password);
            if (userResult.Succeeded) {
                await _userManager.AddToRoleAsync(user, "Customer");
                return Ok(new {Message="Thêm thành công"});
            }
            return BadRequest(userResult.Errors);
        }
    }
}
