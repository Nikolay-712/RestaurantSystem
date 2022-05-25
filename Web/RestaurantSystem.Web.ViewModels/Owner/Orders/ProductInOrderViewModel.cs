namespace RestaurantSystem.Web.ViewModels.Owner.Orders
{
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;

    public class ProductInOrderViewModel : IMapFrom<OrderProducts>
    {
        public string ProductName { get; init; }

        public decimal ProductPrice { get; init; }

        public int Count { get; init; }

        public decimal Sum => this.ProductPrice * this.Count;

    }
}
