using Microsoft.AspNetCore.Identity;
using WebApi.Model;

namespace WebApi.Data.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task<SignInResult> LoginAsync(string email, string password);
        //Task<string> ResetPassWord(string password);

    }
}
