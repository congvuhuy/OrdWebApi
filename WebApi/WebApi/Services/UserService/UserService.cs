using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data.Repository.UserRepository;
using WebApi.DTOs;
using WebApi.Model;
using WebApi.Services.UserService;

namespace WebApi.Services.UserService
{
   
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> RegisterAsync(UserRegisterDTO model)
        {
            var user = new ApplicationUser { 
                UserName = model.Email, 
                Email = model.Email,
                Address = "",
                FullName="",
                DateOfBirth = DateTime.Now, 
                ProfilePictureUrl = "", 
                CreatedAt = DateTime.Now, 
                UpdatedAt =DateTime.Now,
                Description =model.Email 
            };
            var result = await _userRepository.RegisterAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return result;
        }

        public async Task<string> LoginAsync(UserLoginDTO model)
        {
            var result = await _userRepository.LoginAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                return GenerateJwtToken(user);
            }
            return null;
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, string.Join(",", _userManager.GetRolesAsync(user).Result)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}



