using Microsoft.AspNetCore.Identity;
using PAS.Web.Models;

namespace PAS.Web.Data;

public static class DbInitializer
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Student", "Supervisor", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@pas.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Admin",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
        // Assign all existing users as Student (temporary for testing)
        var users = userManager.Users.ToList();

        foreach (var user in users)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                if (user.Email == "supervisor1@gmail.com")
                {
                    await userManager.AddToRoleAsync(user, "Supervisor");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }
        }
    }
}