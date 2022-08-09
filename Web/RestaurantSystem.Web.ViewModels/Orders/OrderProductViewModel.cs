namespace RestaurantSystem.Web.ViewModels.Orders
{
    using AutoMapper;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;

    public class OrderProductViewModel : IMapFrom<OrderProducts>, IHaveCustomMappings
    {
        public string OrderId { get; set; }

        public string ProductId { get; init; }

        public string ProductName { get; init; }

        public decimal ProductPrice { get; init; }

        public string Category { get; init; }

        public int Count { get; init; }

        public decimal Sum => this.ProductPrice * this.Count;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderProducts, OrderProductViewModel>()
               .ForMember(x => x.Category, opt =>
                   opt.MapFrom(x => x.Product.Category));
        }
    }
}
