namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Products;
    using RestaurantSystem.Services.Mapping;

    public class ProductStatisticViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Name { get; init; }

        public int InOrders { get; init; }

        public int Ordered { get; init; }

        public int Votes { get; init; }

        public double AvgRating { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductStatisticViewModel>()
                .ForMember(x => x.InOrders, opt =>
                    opt.MapFrom(x => x.OrderProducts.ToList().Count()))
                .ForMember(x => x.Ordered, opt =>
                    opt.MapFrom(x => x.OrderProducts.Select(x => x.Count).Sum()))
                .ForMember(x => x.Votes, opt =>
                    opt.MapFrom(x => x.Ratings.Count()))
                .ForMember(x => x.AvgRating, opt =>
                    opt.MapFrom(x =>
                    x.Ratings.Select(x => x.Stars).Count() == 0 ? 0 :
                    x.Ratings.Select(x => x.Stars).Average()));
        }
    }
}
