namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;

    public class StatisticViewModel : IMapFrom<Restaurant>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }

        public double AvgRating { get; set; }

        public IEnumerable<OrderStatisticViewModel> Orders { get; set; }

        public IEnumerable<ProductStatisticViewModel> Menu { get; set; }

        public int OrdersCount => this.Orders.Count();

        public int ReservationsCount { get; set; }

        public decimal OrdersRevenu
            => this.Orders.Select(x => x.TotaalSum).Sum();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Restaurant, StatisticViewModel>()
                .ForMember(x => x.Votes, opt =>
                    opt.MapFrom(x => x.Ratings.Count()))
                .ForMember(x => x.AvgRating, opt =>
                    opt.MapFrom(x =>
                    x.Ratings.Select(x => x.Stars).Count() == 0 ? 0 :
                    x.Ratings.Select(x => x.Stars).Average()))
                .ForMember(x => x.ReservationsCount,opt =>
                    opt.MapFrom(x => x.Rservations.Count()));
        }
    }
}
