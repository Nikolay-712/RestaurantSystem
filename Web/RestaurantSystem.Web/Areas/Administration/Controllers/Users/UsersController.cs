namespace RestaurantSystem.Web.Areas.Administration.Controllers.Users
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Users;

    public class UsersController : AdministrationController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Аpprove(string messageId, string approve)
        {
            await this.userService.АpproveUserAsync(messageId, approve);

            return this.RedirectToAction("ReadMessage", "Dashboard", new { messageId = messageId });
        }
    }
}
