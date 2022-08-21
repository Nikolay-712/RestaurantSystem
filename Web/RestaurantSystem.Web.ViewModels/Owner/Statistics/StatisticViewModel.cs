namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;

    public class StatisticViewModel : IMapFrom<Restaurant>, IHaveCustomMappings
    {
        public string Name { get; init; }

        public DateTime CreatedOn { get; init; }

        public int Votes { get; init; }

        public double AvgRating { get; init; }

        public IEnumerable<OrderStatisticViewModel> Orders { get; init; }

        public IEnumerable<ProductStatisticViewModel> Menu { get; init; }

        public int OrdersCount => this.Orders.Count();

        public int RejectedOrdersCount
            => this.Orders.Where(x => x.Status == "Canceled").Count();

        public int ReservationsCount { get; init; }

        public int RejectedReservationsCount { get; init; }

        public decimal OrdersRevenu
            => this.Orders.Select(x => x.TotaalSum).Sum();

        public IEnumerable<MonthlyReport> MonthlyReport
            => this.GetMonthlyReport();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Restaurant, StatisticViewModel>()
                .ForMember(x => x.Votes, opt =>
                    opt.MapFrom(x => x.Ratings.Count()))
                .ForMember(x => x.AvgRating, opt =>
                    opt.MapFrom(x =>
                    x.Ratings.Select(x => x.Stars).Count() == 0 ? 0 :
                    x.Ratings.Select(x => x.Stars).Average()))
                .ForMember(x => x.ReservationsCount, opt =>
                    opt.MapFrom(x => x.Rservations.Count()))
                .ForMember(x => x.RejectedReservationsCount, opt =>
                    opt.MapFrom(x => x.Rservations
                    .Where(x => x.ReservationStatus == ReservationStatus.Canceled).Count()));
        }

        private IEnumerable<MonthlyReport> GetMonthlyReport()
        {
            var monthlyReport = new List<MonthlyReport>();

            for (int month = 1; month <= 12; month++)
            {
                monthlyReport.Add(new MonthlyReport
                {
                    Month = month.ToString(),
                    OrdersCount = this.Orders.Where(x => x.CreatedOn.Month == month).Count(),
                    OrdersRevenu = this.Orders.Where(x => x.CreatedOn.Month == month).Select(x => x.TotaalSum).Sum(),
                });
            }

            return monthlyReport;
        }
    }
}
