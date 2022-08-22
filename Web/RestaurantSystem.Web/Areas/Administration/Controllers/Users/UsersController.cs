namespace RestaurantSystem.Web.Areas.Administration.Controllers.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Models.Users;
    using RestaurantSystem.Services.Users;
    using RestaurantSystem.Web.ViewModels.Administration.Users;

    using static RestaurantSystem.Common.GlobalConstants;

    public class UsersController : AdministrationController
    {
        private readonly IUserService userService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(IUserService userService, RoleManager<ApplicationRole> roleManager)
        {
            this.userService = userService;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Аpprove(string messageId, string approve)
        {
            var result = await this.userService.АpproveUserAsync(messageId, approve);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("ReadMessage", "Dashboard", new { messageId = messageId });
        }

        public async Task<IActionResult> Owners()
        {
            var ownerRole = await this.roleManager.FindByNameAsync(OwnerRoleName);

            var owners = this.userService
                .AllUsers<OwnerViewModel>()
                .Where(x => x.Roles.Contains(ownerRole.Id));

            return this.View(owners);
        }
    }
}
