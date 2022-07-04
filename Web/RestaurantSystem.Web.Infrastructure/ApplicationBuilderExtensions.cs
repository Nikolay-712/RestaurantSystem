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

                    string roleEmail = $"{roleName.ToLower()}@abv.bg";
                    string rolePassword = $"{roleName.ToLower()}123";

                    var user = new ApplicationUser
                    {
                        Email = roleEmail,
                        UserName = roleEmail,
                    };

                    await userManager.CreateAsync(user, rolePassword);
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
