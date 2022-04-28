namespace RestaurantSystem.Services.Users
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> АpproveUserAsync(string messageId);
    }
}
