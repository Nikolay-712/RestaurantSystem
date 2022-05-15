namespace RestaurantSystem.Services.Orders
{
    using System.Threading.Tasks;

    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Web.ViewModels.Orders;

    public interface IOrderService
    {
        Task<string> MakeOrderAsync(string restaurantId, string userId);

        Order GetUserOrder(string userId, string restaurantId);

        Task AddProductAsync(string orderId, string productId, string userId, string restaurantId);

        OrderViewModel GetProductsInOrder(string userId, string restaurantId);
    }
}
