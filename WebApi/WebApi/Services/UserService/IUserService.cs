using Microsoft.AspNetCore.Identity;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.UserService
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDTO model);
        Task<string> LoginAsync(UserLoginDTO model);
    }
}
