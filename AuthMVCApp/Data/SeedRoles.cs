using Microsoft.AspNetCore.Identity;

namespace AuthMVCApp.Data
{
    public static class SeedRoles
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] roleNames = { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var adminUser = await userManager.FindByNameAsync("admin");
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "veronika.khimenko@icloud.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(adminUser, "AdminPassword123!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
        
    }
}
