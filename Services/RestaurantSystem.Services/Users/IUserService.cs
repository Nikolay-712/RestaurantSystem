﻿namespace RestaurantSystem.Services.Users
{
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models;

    public interface IUserService
    {
        Task<bool> АpproveUserAsync(string messageId);

        Task SavePhoneNumberAsync(string userId, string phoneNumber);

        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}