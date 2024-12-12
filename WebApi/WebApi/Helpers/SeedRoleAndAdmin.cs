using Microsoft.AspNetCore.Identity;
using WebApi.Model;
namespace WebApi.Helpers
{
    public static class SeedRoleAndAdmin
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roleNames = { "Customer", "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!userManager.Users.Any(u => u.Email == "admin@.com"))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@.com",
                    Email = "admin@.com",
                    Address = "",
                    FullName = "",
                    DateOfBirth = DateTime.Now,
                    ProfilePictureUrl = "",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Description ="Day la Admin"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123456");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
