namespace RestaurantSystem.Data.Models.Orders
{
    using RestaurantSystem.Data.Models.Products;

    public class OrderProducts
    {
        public string OrderId { get; set; }

        public Order Order { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
