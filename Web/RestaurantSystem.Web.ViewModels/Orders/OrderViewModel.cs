namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }

        public decimal TotaalSum => this.OrderProducts.Select(x => x.Sum).Sum();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.OrderProducts, opt =>
                    opt.MapFrom(x => x.OrderProducts));
        }
    }
}
