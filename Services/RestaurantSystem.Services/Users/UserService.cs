﻿namespace RestaurantSystem.Services.Users
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


            return true;

        }

        public async Task SavePhoneNumberAsync(string userId, string phoneNumber)
        {
            var user = await this.GetUserByIdAsync(userId);

            user.PhoneNumber = "+359" + phoneNumber;

            this.applicationDbContext.Update(user);
            await this.applicationDbContext.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await this.userManager.FindByIdAsync(userId);
        }
    }
}
