namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    using System.Collections.Generic;
    using System.Linq;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Owner.Orders;
    using RestaurantSystem.Web.ViewModels.Owner.Reservations;

    public class RestaurantDetailsViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public IEnumerable<ReservationViewModel> Rservations { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public int PendingReservationsCount
            => this.Rservations.Where(x => x.ReservationStatus == "Pending").Count();

        public int PendingOrdersCount
            => this.Orders.Where(x => x.Status == "Pending").Count();
    }
}
