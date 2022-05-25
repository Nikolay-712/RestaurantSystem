namespace RestaurantSystem.Web.ViewModels.Owner.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Data.Models.Payments;
    using RestaurantSystem.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Status { get; init; }

        public string ResaurantId { get; init; }

        public string UserId { get; init; }

        public string PhoneNumber { get; init; }

        public string PaymentId { get; init; }

        public Payment Payment { get; init; }

        public string ShippingAddress { get; init; }

        public bool IsReady { get; set; }

        public IEnumerable<ProductInOrderViewModel> OrderProducts { get; init; }

        public decimal TotaalSum => this.OrderProducts.Select(x => x.Sum).Sum();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.OrderProducts, opt =>
                    opt.MapFrom(x => x.OrderProducts));
        }
    }
}
