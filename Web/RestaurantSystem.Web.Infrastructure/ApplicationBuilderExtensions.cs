namespace RestaurantSystem.Web.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RestaurantSystem.Data.Models.Users;

    public static class ApplicationBuilderExtensions
    {
        public static void SeedApplicationRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole { Name = roleName };
                    await roleManager.CreateAsync(role);

                    string adminEmail = $"{roleName}@abv.bg";
                    string adminPassword = $"{roleName}123";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
