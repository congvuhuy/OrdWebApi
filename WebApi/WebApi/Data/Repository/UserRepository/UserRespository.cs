using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApi.Data.Repository.UserRepository;
using WebApi.Model;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
    {

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }
        return SignInResult.Failed;
    }
}
