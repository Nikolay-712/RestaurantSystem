namespace RestaurantSystem.Services.Users
{
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Users;
    using RestaurantSystem.Web.ViewModels.Addresses;

    public interface IUserService
    {
        Task<bool> АpproveUserAsync(string messageId, string appruve);

        Task SavePhoneNumberAsync(string userId, string phoneNumber);

        Task SaveAddressAsync(string userId, AddresInputModel addresInput);

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Address GetUserAddress(string userId);
    }
}
