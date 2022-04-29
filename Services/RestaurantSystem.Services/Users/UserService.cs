namespace RestaurantSystem.Services.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Web.ViewModels.Administration.Messages;

    using static RestaurantSystem.Common.GlobalConstants;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContactService contactService;

        public UserService(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            IContactService contactService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            this.contactService = contactService;
        }

        public async Task<bool> АpproveUserAsync(string messageId)
        {
            var approveResult = true;

            var message = this.applicationDbContext
                .Messages
                .FirstOrDefault(x => x.Id == messageId);

            if (message == null)
            {
                return approveResult = false;
            }

            var user = await this.userManager.FindByEmailAsync(message.Sender);
            var result = await this.userManager.AddToRoleAsync(user, OwnerRoleName);

            if (result.Succeeded)
            {
                var answer = new AdminMessageViewModel
                {
                    Id = messageId,
                    Text = "Вече може да използвате Owner Area",
                    Sender = message.Sender,
                };

                await this.contactService.ReturnАnswerAsync(answer);
            }

            return approveResult = result.Succeeded;

        }
    }
}
