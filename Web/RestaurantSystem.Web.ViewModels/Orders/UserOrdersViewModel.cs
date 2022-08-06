namespace RestaurantSystem.Web.ViewModels.Orders
{
    using AutoMapper;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserOrdersViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string Id { get; init; }

        public string ResaurantId { get; init; }

        public DateTime CreatedOn { get; init; }

        public OrderStatus Status { get; set; }

        public string OrderNumber => this.Id.Substring(0, 4);

        public IEnumerable<OrderProductViewModel> OrderProducts { get; init; }

        public decimal TotaalSum => this.OrderProducts.Select(x => x.Sum).Sum();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, UserOrdersViewModel>()
                .ForMember(x => x.OrderProducts, opt =>
                    opt.MapFrom(x => x.OrderProducts));
        }
    }
}
