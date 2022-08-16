namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Orders;

    public class OrderStatisticViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string Status { get; init; }

        public IEnumerable<ProductInOrderViewModel> OrderProducts { get; init; }

        public decimal TotaalSum => this.OrderProducts.Select(x => x.Sum).Sum();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderStatisticViewModel>()
                .ForMember(x => x.OrderProducts, opt =>
                    opt.MapFrom(x => x.OrderProducts));
        }
    }
}
